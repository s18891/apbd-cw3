using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        //[RegularExpression("^s[0-9]+$")]
        [Required(ErrorMessage = "S number not recieved")]
        [RegularExpression("^s[0-9]+$", ErrorMessage = "wrong S number")]
        public string IndexNumber { get; set; }

        [Required(ErrorMessage = "Name not recieved")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname not recieved")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Range(typeof(DateTime), "1/1/1900", "01/01/2100", ErrorMessage = "Wrong birth date")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "name of studies not recieved")]
        [MaxLength(100)]
        public string Studies { get; set; }


    }
}
