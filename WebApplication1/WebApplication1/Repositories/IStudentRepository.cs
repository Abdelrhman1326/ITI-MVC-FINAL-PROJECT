using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Repositories;

public interface IStudentRepository 
{
    public IEnumerable<Student> GetStudents();
    public Student GetStudent(int id);
    public void CreateStudent(Student student);
    public void EditStudent(Student student);
    public void DeleteStudent(int id);
    public void Save();
}