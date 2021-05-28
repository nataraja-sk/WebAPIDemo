using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPIDemo.DataBase;
using WebAPIDemo.Helpers;
using WebAPIDemo.Interface;
using WebAPIDemo.Models;
using WebAPIDemo.Resources;

namespace WebAPIDemo.Controllers
{
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase, IStudentInterface
    {
        private Database db = new Database();

        private readonly ILogger<Test> _logger;

        public StudentController(ILogger<Test> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Get Students list
        /// </summary>
        /// <returns></returns>

        [Route("api/Students/GetAllStudents")]
        [HttpGet]
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
        [Route("api/Students/GetStudent/{ID}")]
        [HttpGet]
        public StudentDetailsResponse GetStudentDetails([Required] int ID)
        {
            StudentDetailsResponse studentsResponse = new StudentDetailsResponse();


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

            return studentsResponse;

        }
        /// <summary>
        /// To update a student data
        /// </summary>
        /// <param name="studentDetailsUpdateRequest"></param>
        /// <returns></returns>
        [Route("api/Students/UpdateStudent")]
        [HttpPost]
        public bool UpdateStudent(StudentDetailsUpdateRequest studentDetailsUpdateRequest)
        {
            bool updated = false;
            if (ModelState.IsValid)
            {
                string query = Queries.GetQuery(200);
                query=query.Replace("{id}", studentDetailsUpdateRequest.ID.ToString());
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
                    query = query.Replace("{dob}", studentDetailsUpdateRequest.DOB.GetValueOrDefault().Year.ToString() + "/" + (studentDetailsUpdateRequest.DOB.GetValueOrDefault().Month).ToString() + "/" +studentDetailsUpdateRequest.DOB.GetValueOrDefault().Day.ToString());
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
                int rows = db.UpdateData(query);
                updated = rows>0?true:false;
            }
            return updated;

        }
        /// <summary>
        /// To delete a student
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("api/Students/DeleteStudent")]
        [HttpPost]
        public bool DeleteStudent([Required] int ID)
        {
            bool deleted = false;
            if (ModelState.IsValid)
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
        [HttpPost]
        [Route("api/Students/AddStudent")]
        public bool AddStudent(AddStudentRequest studentDetails)
        {
            bool added = false;
            if (ModelState.IsValid)
            {
                string query = String.Format(Queries.GetQuery(100), studentDetails.Name, studentDetails.DOB.Year.ToString() + "/" + (studentDetails.DOB.Month).ToString() +"/"+ studentDetails.DOB.Day.ToString(),studentDetails.Score.GetValueOrDefault());

                int rows = db.UpdateData(query);
                added = rows > 0 ? true : false;
            }
            return added;

        }

    }
}
