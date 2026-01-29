using MyApp.Namespace.Models;
using MyApp.Namespace.Data;

namespace MyApp.Namespace.Services
{
    public class FoxService : IFoxService
    {
        private readonly IFoxesRepository _repo;

        public FoxService(IFoxesRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Fox> GetAll()
        {
            var foxes = _repo.GetAll() ?? Enumerable.Empty<Fox>();
            return foxes
                .OrderByDescending(f => f.Loves)
                .ThenBy(f => f.Hates);
        }

        public Fox? Get(int id)
        {
            if (id <= 0)
                return null;
            return _repo.Get(id);
        }

        public Fox Add(Fox fox)
        {
            _repo.Add(fox);
            return fox;
        }

        public bool Update(int id, Fox fox)
        {
            var existingFox = _repo.Get(id);
            if (existingFox == null)
            {
                return false;
            }
            _repo.Update(id, fox);
            return true;
        }

        public Fox? Love(int id)
        {
            var fox = _repo.Get(id);
            if (fox == null)
            {
                return null;
            }
            fox.Loves++;
            _repo.Update(id, fox);
            return fox;
        }

        public Fox? Hate(int id)
        {
            var fox = _repo.Get(id);
            if (fox == null)
            {
                return null;
            }
            fox.Hates++;
            _repo.Update(id, fox);
            return fox;
        }
    }
}