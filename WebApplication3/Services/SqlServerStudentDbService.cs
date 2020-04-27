using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DTOs.Requests;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class SqlServerStudentDbService : IStudentDbServeice
    {
        public void EnrollStudent(EnrollStudentRequest request)
        {

            /*  
             *  inna starsza walidacja będów zamiast [ApiController]
             *  
             *  if (!ModelState.IsValid)
              {
                  var d = ModelState;
                  return BadRequest("!!!");
              }

      */



            //DTOs data transfer objects
            //request models
            //==mapowanie
            // Modele biznesowe 
            //==mapowanie
            //response models
            var st = new Student();
            st.FirstName = request.FirstName;
            //...
            //...
            using (SqlConnection con = new SqlConnection(""))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    //1. Czy studia istnieja?
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //..
                    }

                    int idstudies = (int)dr["IdStudies"];

                    //x. Dodanie studenta

                    com.CommandText = "INSERT INTO Student(IndexNumber,FirstName) VALUES(@Index, @Fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    //....
                    com.ExecuteNonQuery();

                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }
            }

        public Student GetStudent(string IndexNumber)
        {
            if (IndexNumber == "s1234")
            {
                return new Student { IndexNumber = "1", FirstName = "Jan", LastName = "Kowalski" };
            }

            return null;
        }

        public void PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }
    }
}
