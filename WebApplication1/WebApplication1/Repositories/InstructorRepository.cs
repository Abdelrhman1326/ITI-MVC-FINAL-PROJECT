using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Repositories;

public class InstructorRepository : ICrudRepository<Instructor>
{
    private readonly ApplicationDbContext _context;
    
    public InstructorRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Instructor> GetAll()
    {
        return _context.Instructors.ToList(); 
    }
    
    public Instructor GetOne(int id)
    {
        return _context.Instructors
            .Include(i => i.Department) // Load the linked Department
            .Include(i => i.Course)     // Load the linked Course
            .FirstOrDefault(i => i.Id == id);
    }
    
    public void Create(Instructor instructor)
    {
        _context.Instructors.Add(instructor);
    }
    
    public void Edit(Instructor instructor)
    {
        _context.Instructors.Update(instructor);
    }

    public void Delete(int id)
    {
        var instructorToDelete = _context.Instructors.Find(id);
        if (instructorToDelete != null)
        {
            _context.Instructors.Remove(instructorToDelete);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}