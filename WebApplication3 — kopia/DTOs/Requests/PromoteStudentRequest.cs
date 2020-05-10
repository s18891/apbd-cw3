using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required(ErrorMessage = "Name of studies not recieved")]
        public string Studies { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Wrong number of semester")]
        public int Semester { get; set; }
    }
}
