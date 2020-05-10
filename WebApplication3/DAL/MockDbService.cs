using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Services
{

    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        public MockDbService()
        {
            /*_students = new List<Student> {
                new Student{IdStudent ...
            };*/
            _students = new List<Student>();
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
