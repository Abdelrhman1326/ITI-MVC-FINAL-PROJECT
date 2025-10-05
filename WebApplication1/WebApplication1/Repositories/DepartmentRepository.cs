using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Repositories;

public class DepartmentRepository : ICrudRepository<Department>
{
    private readonly ApplicationDbContext _context;
    
    // Constructor injection:
    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Department> GetAll()
    {
        return _context.Departments.ToList(); 
    }
    
    public Department GetOne(int id)
    {
        return _context.Departments
            .Include(d => d.Courses)        // Eagerly loads all linked courses
            .Include(d => d.Students)       // Eagerly loads all linked students
            .Include(d => d.Instructors)    // Eagerly loads all linked instructors
            .FirstOrDefault(d => d.Id == id);
    }
    
    public void Create(Department department)
    {
        _context.Departments.Add(department);
    }
    
    public void Edit(Department department)
    {
        _context.Departments.Update(department);
    }

    public void Delete(int id)
    {
        var departmentToDelete = _context.Departments.Find(id);
        if (departmentToDelete != null)
        {
            _context.Departments.Remove(departmentToDelete);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}