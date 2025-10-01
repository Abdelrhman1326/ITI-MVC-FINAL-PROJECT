using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Linq;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;
    
    // constructor injection 
    public StudentRepository(ApplicationDbContext context) 
    {
        _context = context;
    }
    
    public IEnumerable<Student> GetStudents()
    {
        return _context.Students.ToList(); 
    }
    
    public Student GetStudent(int id)
    {
        return _context.Students
            .Include(s => s.Department)
            .FirstOrDefault(s => s.StudentId == id);
    }

    public void CreateStudent(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void EditStudent(Student student)
    {
        _context.Students.Update(student);
    }

    public void DeleteStudent(int id)
    {
        var student = GetStudent(id);
        _context.Students.Remove(student);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}