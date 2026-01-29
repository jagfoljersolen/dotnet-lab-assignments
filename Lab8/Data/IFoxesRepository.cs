using MyApp.Namespace.Models;

namespace MyApp.Namespace.Data
{
public interface IFoxesRepository
{
    IEnumerable<Fox> GetAll();

    Fox? Get(int id);
    void Add(Fox f);
    
    void Update(int id, Fox f);
}
}