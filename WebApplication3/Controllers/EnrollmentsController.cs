using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.DTOs;
using WebApplication3.DTOs.Requests;
using WebApplication3.DTOs.Responses;
using System.Data.SqlClient;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            this._service = service;
        }

        [HttpPost]
        public IActionResult SignUpStudentForStudies(EnrollStudentRequest request)
        {
            string response = _service.enrollStudent(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/enrollments/promotions")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            List<Enrollment> list = _service.promoteStudents(request);
            return Ok(list);
        }
    }
}