using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public abstract class AbstractStudentRepo
    {
        public const string CONSTANT_ABSTRACT = "dddd";
        public abstract Student GetStudentById(int StudentId);
        public abstract List<Student> GetAllStudent();
    }
}
