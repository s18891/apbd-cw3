using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs.Requests;
using WebApplication3.Models;

namespace WebApplication3.Services

{
    public class SqlServerStudentDbService : IStudentDbService
{
    private const string connString = "Data Source=db-mssql;Initial Catalog=s18891;Integrated Security=True";

        public string enrollStudent(EnrollStudentRequest request)
        {
            using (var connection = new SqlConnection(connString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "select IdStudy from Studies where name=@name";
                command.Parameters.AddWithValue("name", request.Studies);

                var dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    transaction.Rollback();
                    return "Studia nie istnieja";
                }
                int idStudies = (int)dr["IdStudy"];

                command.CommandText = "select IndexNumber from Student where IndexNumber=@IndexNumber";
                command.Parameters.AddWithValue("IndexNumber", request.IndexNumber);

                dr.Close();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    transaction.Rollback();
                    return "Student o podanym indeksie juz istnieje";
                }

                int IdEnrollment;
                command.CommandText = "select * from Enrollment where IdStudy=@IdStudies and Semester = 1";
                command.Parameters.AddWithValue("IdStudies", idStudies);


                dr.Close();
                dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    command.CommandText = "select max(IdEnrollment) from Enrollment";
                    var reader = command.ExecuteReader();
                    IdEnrollment = (int)reader["IdEnrollment"];
                    IdEnrollment++;

                    command.CommandText = "Insert into Enrollment(IdEnrollment, Semester, IdStudy, StartDate) values(@IdEnrollment, 1, @IdStudies, GETDATE())";
                    command.Parameters.AddWithValue("IdEnrollment", IdEnrollment);
                    command.Parameters.AddWithValue("IdStudies", idStudies);
                    command.ExecuteReader();
                }
                IdEnrollment = (int)dr["IdEnrollment"];
                command.CommandText = "Insert into Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values(@IndexNumber, @FirstName, @Lastname, @BirthDate, @IdEnrollment)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                command.Parameters.AddWithValue("FirstName", request.FirstName);
                command.Parameters.AddWithValue("LastName", request.LastName);
                command.Parameters.AddWithValue("BirthDate", request.BirthDate);
                command.Parameters.AddWithValue("IdEnrollment", IdEnrollment);

                dr.Close();
                command.ExecuteNonQuery();
                dr.Close();

                transaction.Commit();
                dr.Close();
                return "Dodano studenta do bazy danych";
            }
        }

        public List<Enrollment> promoteStudents(PromoteStudentRequest request)
        {
            List<Enrollment> list = new List<Enrollment>();
            using (var connection = new SqlConnection(connString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "exec PromoteStudents @StudiesName, @Semester";
                command.Parameters.AddWithValue("StudiesName", request.Studies);
                command.Parameters.AddWithValue("Semester", request.Semester);

                command.ExecuteNonQuery();
                command.CommandText = "Select * from Enrollment";
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Enrollment enrollment = new Enrollment();
                    enrollment.IdEnrollment = (string)dr["IdEnrollment"];
                    enrollment.Semester = (string)dr["Semester"];
                    enrollment.IdStudy = (string)dr["IdStudy"];
                    enrollment.StartDate = (string)dr["StartDate"];
                    list.Add(enrollment);
                }
            }
            return list;
        }
    }
}