using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.DataBase;
using WebAPIDemo.Helpers;
using WebAPIDemo.Interface;
using WebAPIDemo.Models;
using WebAPIDemo.Resources;

namespace WebAPIDemo.Services
{
    public class StudentService : IStudentInterface
    {
        private Database db = new Database();
        public StudentDetailsResponse GetAllStudents()
        {
            StudentDetailsResponse studentsResponse = new StudentDetailsResponse();
            string query = Queries.GetQuery(1);
            DataTable allStudents = db.GetData(query);
            if (allStudents != null && allStudents.Rows != null && allStudents.Rows.Count > 0)
            {
                studentsResponse.students = Utilities.ConvertDataTable<Student>(allStudents);
            }



            return studentsResponse;

        }
        /// <summary>
        /// Get details of particular student
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public StudentDetailsResponse GetStudentDetails(int ID)
        {
            StudentDetailsResponse studentsResponse = new StudentDetailsResponse();
            if (ID > 0)
            {
                string query = string.Format(Queries.GetQuery(2), ID.ToString());
                DataTable studentDT = db.GetData(query);
                if (studentDT != null && studentDT.Rows != null && studentDT.Rows.Count > 0)
                {
                    studentsResponse.students = Utilities.ConvertDataTable<Student>(studentDT);
                }
                else
                {
                    //throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { Content=new StringContent("ID " + ID + " does not exists."),ReasonPhrase="Student details not found" });
                }
            }


            return studentsResponse;

        }
        /// <summary>
        /// To update a student data
        /// </summary>
        /// <param name="studentDetailsUpdateRequest"></param>
        /// <returns></returns>
        public bool UpdateStudent(StudentDetailsUpdateRequest studentDetailsUpdateRequest)
        {
            bool updated = false;
            if (studentDetailsUpdateRequest != null && studentDetailsUpdateRequest.ID > 0)
            {
                string query = Queries.GetQuery(200);

                #region Update query parameters
                query = query.Replace("{id}", studentDetailsUpdateRequest.ID.ToString());
                if (!string.IsNullOrWhiteSpace(studentDetailsUpdateRequest.Name))
                {
                    query = query.Replace("{name}", studentDetailsUpdateRequest.Name);
                }
                else
                {
                    query = query.Replace("'{name}'", "NULL");
                }
                if (studentDetailsUpdateRequest.DOB != null)
                {
                    query = query.Replace("{dob}", studentDetailsUpdateRequest.DOB.GetValueOrDefault().Year.ToString() + "/" + (studentDetailsUpdateRequest.DOB.GetValueOrDefault().Month).ToString() + "/" + studentDetailsUpdateRequest.DOB.GetValueOrDefault().Day.ToString());
                }
                else
                {
                    query = query.Replace("'{dob}'", "NULL");
                }
                if (studentDetailsUpdateRequest.Score != null)
                {
                    query = query.Replace("{score}", studentDetailsUpdateRequest.Score.ToString());
                }
                else
                {
                    query = query.Replace("{score}", "NULL");
                }
                #endregion
                int rows = db.UpdateData(query);
                updated = rows > 0 ? true : false;
            }
            return updated;

        }
        /// <summary>
        /// To delete a student
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteStudent(int ID)
        {
            bool deleted = false;
            if (ID > 0)
            {
                string query = String.Format(Queries.GetQuery(300), ID);

                int rows = db.UpdateData(query);
                deleted = rows > 0 ? true : false;
            }
            return deleted;

        }
        /// <summary>
        /// To Add a student
        /// </summary>
        /// <param name="studentDetails"></param>
        /// <returns></returns>
        public bool AddStudent(AddStudentRequest studentDetails)
        {
            bool added = false;
            if (studentDetails != null)
            {
                string query = String.Format(Queries.GetQuery(100), studentDetails.Name, studentDetails.DOB.Year.ToString() + "/" + (studentDetails.DOB.Month).ToString() + "/" + studentDetails.DOB.Day.ToString(), studentDetails.Score.GetValueOrDefault());

                int rows = db.UpdateData(query);
                added = rows > 0 ? true : false;
            }
            return added;

        }

    }
}
