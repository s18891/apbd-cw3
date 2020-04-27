using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students = new List<Student>
        { 
            new Student{ FirstName="Jan", LastName="Kowalski", IndexNumber="s1234" },
            new Student{ FirstName="Jan", LastName="Kowalski11111", IndexNumber="s1248" },
            new Student{/*IdStudent=1,*/ FirstName="Jan", LastName="Kowalski2222222", IndexNumber="s1964" }


        };

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
