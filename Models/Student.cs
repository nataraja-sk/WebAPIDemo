using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.Models
{
   
    /// <summary>
    /// To Hold student details
    /// </summary>
    public class Student
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public DateTime? DOB { get; set; }
        public int? Score { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
    /// <summary>
    /// To Hold student details response
    /// </summary>
    public class StudentDetailsResponse
    {
        public List<Student> students { get; set; }
    }
    /// <summary>
    /// Request to update student detaiils
    /// </summary>
    public class StudentDetailsUpdateRequest
    {
        [Required]
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime? DOB { get; set; }
        public int? Score { get; set; }
    }
    public class AddStudentRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        public int? Score { get; set; }
    }
}
