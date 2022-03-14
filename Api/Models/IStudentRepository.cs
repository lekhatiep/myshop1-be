using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public interface IStudentRepository
    {
        public const string CONSTANT_INTERFACE = "dddd";
        Student GetStudentById(int StudentId);
        List<Student> GetAllStudent();

        string OftenChange();
    }
}
