using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Repositories;

public interface IStudentRepository 
{
    IEnumerable<Student> GetStudents();
    Student GetStudent(int id);
}