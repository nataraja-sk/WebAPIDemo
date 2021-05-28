using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;

namespace WebAPIDemo.Interface
{
    public interface IStudentInterface
    {
        StudentDetailsResponse GetAllStudents();
        StudentDetailsResponse GetStudentDetails([Required] int ID);
        bool UpdateStudent(StudentDetailsUpdateRequest studentDetailsUpdateRequest);
        bool DeleteStudent([Required] int ID);
        bool AddStudent([Required] AddStudentRequest studentDetails);
    }
}
