using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Linq;

public class StudentRepository : ICrudRepository<Student>
{
    private readonly ApplicationDbContext _context;
    
    // constructor injection 
    public StudentRepository(ApplicationDbContext context) 
    {
        _context = context;
    }
    
    public IEnumerable<Student> GetAll()
    {
        return _context.Students.ToList(); 
    }
    
    public Student GetOne(int id)
    {
        return _context.Students
            .Include(s => s.Department)
            .FirstOrDefault(s => s.StudentId == id);
    }

    public void Create(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void Edit(Student student)
    {
        _context.Students.Update(student);
    }

    public void Delete(int id)
    {
        var student = GetOne(id);
        _context.Students.Remove(student);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}