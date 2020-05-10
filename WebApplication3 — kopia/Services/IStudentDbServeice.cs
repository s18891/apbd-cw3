using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DTOs.Requests;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IStudentDbServeice
    {

        void EnrollStudent(EnrollStudentRequest request);
        void PromoteStudents(int semester, string studies);

        Student GetStudent(string IndexNumber);
    }
}
