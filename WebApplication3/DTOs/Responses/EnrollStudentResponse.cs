using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        [Required]
        public string Studies { get; set; }
        [Required]
        public string Semester { get; set; }
    }
}
