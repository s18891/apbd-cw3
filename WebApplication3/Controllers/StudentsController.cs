﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;


namespace WebApplication3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18891;Integrated Security=True";

        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student";

                con.Open();
                SqlDataReader dr=com.ExecuteReader();
                while (dr.Read())
                {

                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    list.Add(st);


                }

            }

            return Ok(list);
        }


        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(String indexNumber)
        {

            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student where indexnumber=@index";

                com.Parameters.AddWithValue("index", indexNumber);


                con.Open();
                con.Open();


                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {

                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    list.Add(st);


                }
            }

                return Ok();
        }















        /*
        private IDbService _dbService;
        public StudentsController(IDbService service)
        {
            _dbService = service;
        }


        //2. QueryString
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            if(orderBy == "lastname")
            return Ok(_dbService.GetStudents().OrderBy(s=> s.LastName));

            return Ok(_dbService.GetStudents());

        }


        //URL segment
        [HttpGet("{id}")]

        public IActionResult GetStudent(int id)
        {
            if(id ==1)
            return Ok("A");

            return NotFound("Student Not found");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student) {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";

            return Ok(student); //JSON
        }

    */
    }
}