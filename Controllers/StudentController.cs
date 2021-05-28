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
    public class StudentController : ControllerBase
    {
        private readonly ILogger<Student> _logger;
        private IStudentInterface _studentService;
        public StudentController(ILogger<Student> logger, IStudentInterface studentInterface)
        {
            _logger = logger;
            _studentService = studentInterface;

        }
        private Database db = new Database();

        /// <summary>
        /// Get Students list - Async
        /// </summary>
        /// <returns></returns>

        [Route("api/Students/GetAllStudents")]
        [ProducesResponseType(typeof(StudentDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var task = Task.Run(() => _studentService.GetAllStudents());
            StudentDetailsResponse studentsResponse = await task;
            if (studentsResponse == null || studentsResponse.students==null || studentsResponse.students.Count==0)
            {
                return NoContent();
            }
            return Ok(studentsResponse);

        }
        /// <summary>
        /// Get details of particular student(async task)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("api/Students/GetStudent/{ID}")]
        [ProducesResponseType(typeof(StudentDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetStudentDetails([Required] int ID)
        {
            StudentDetailsResponse studentsResponse = new StudentDetailsResponse();
            if (ID > 0)
            {
                var task = Task.Run(() => _studentService.GetStudentDetails(ID));
                studentsResponse = await task;
                if (!(studentsResponse != null && studentsResponse.students != null && studentsResponse.students.Count > 0))
                {
                    return NoContent();
                }
            }


            return Ok(studentsResponse);

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
            if (studentDetailsUpdateRequest != null && studentDetailsUpdateRequest.ID > 0)
            {
                updated = _studentService.UpdateStudent(studentDetailsUpdateRequest);
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
            if (ID > 0)
            {
                deleted = _studentService.DeleteStudent(ID);
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
            if (studentDetails != null)
            {
                added = _studentService.AddStudent(studentDetails);
            }
            return added;

        }

    }
}
