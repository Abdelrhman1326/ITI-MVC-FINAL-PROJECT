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
}