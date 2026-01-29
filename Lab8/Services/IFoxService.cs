using MyApp.Namespace.Models;
using MyApp.Namespace.Data;


namespace MyApp.Namespace.Services
{
    public interface IFoxService
    {
        IEnumerable<Fox> GetAll();
        Fox? Get(int id);
        Fox Add(Fox fox);
        bool Update(int id, Fox fox);
        Fox? Love(int id);
        Fox? Hate(int id);
    }
}