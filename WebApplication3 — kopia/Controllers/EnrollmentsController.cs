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
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbServeice _service;

        public EnrollmentsController(IStudentDbServeice serveice)
        {
            _service = serveice;
        }





[HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            

                var response = new EnrollStudentResponse();
            //response.LastName = st.LastName;
            //...


            return Ok(response);
        }

    }
}