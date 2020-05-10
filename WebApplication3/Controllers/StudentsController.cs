using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;


namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService dbService;
        private ICollection<Student> _students;
        string connString = "Data Source=db-mssql;Initial Catalog=s18891;Integrated Security=True";

        public StudentsController(IDbService dbService)
        {
            this.dbService = dbService;
            this._students = (List<Student>)dbService.GetStudents();
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {

            using (var connection = new SqlConnection(connString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, e.Semester, st.Name from Student s, Enrollment e, Studies st where s.IdEnrollment = e.IdEnrollment and e.IdStudy = st.IdStudy;";

                connection.Open();
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    var student = new Student
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        SemesterNumber = dr["Semester"].ToString(),
                        StudiesName = dr["Name"].ToString()
                    };
                    _students.Add(student);
                }
                return Ok(_students);
            }
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            using (var connection = new SqlConnection(connString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select s.FirstName, s.LastName, e.Semester, e.StartDate from Student s, Enrollment e where s.IdEnrollment = e.IdEnrollment and s.IndexNumber = @index";
                command.Parameters.AddWithValue("index", indexNumber);
                connection.Open();

                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    var student = new Student
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        SemesterNumber = dr["Semester"].ToString()
                    };
                }
                return Ok(_students);
            }
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            //student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok(200 + " Usuwanie zakonczone");
        }
    }
}