namespace WebApplication1.Repositories;

public interface ICrudRepository<T>
{
    public IEnumerable<T> GetAll();
    public T GetOne(int id);
    public void Create(T t);
    public void Edit(T t);
    public void Delete(int id);
    public void Save();
}