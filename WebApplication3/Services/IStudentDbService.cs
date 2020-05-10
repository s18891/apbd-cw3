using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DTOs.Requests;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IStudentDbService
    {
        public string enrollStudent(EnrollStudentRequest request);
        public List<Enrollment> promoteStudents(PromoteStudentRequest request);
    }
}
