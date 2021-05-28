using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Interface;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class Test : ControllerBase
    {
       

        private readonly ILogger<Test> _logger;
        private IStudentInterface _studentService;
        public Test(ILogger<Test> logger, IStudentInterface studentInterface)
        {
            _logger = logger;
            _studentService = studentInterface;

        }

        [HttpGet]
        private dynamic Get()
        {
            
            return _studentService.AddStudent(new AddStudentRequest() { Name="ABCD",DOB=DateTime.Now.AddDays(-100)});
        }
    }
}
