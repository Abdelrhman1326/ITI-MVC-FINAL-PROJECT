using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

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
        // Retrieves all Department records
        return _context.Departments.ToList(); 
    }
    
    public Department GetOne(int id)
    {
        // Finds a single Department by its primary key (Id)
        return _context.Departments.Find(id);
    }
    
    public void Create(Department department)
    {
        // Adds a new Department entity to the DbSet
        _context.Departments.Add(department);
    }
    
    public void Edit(Department department)
    {
        // Marks the Department entity as modified for update
        _context.Departments.Update(department);
    }

    public void Delete(int id)
    {
        // Finds the Department and removes it if it exists
        var departmentToDelete = _context.Departments.Find(id);
        if (departmentToDelete != null)
        {
            _context.Departments.Remove(departmentToDelete);
        }
    }

    public void Save()
    {
        // Persists all pending changes to the database
        _context.SaveChanges();
    }
}