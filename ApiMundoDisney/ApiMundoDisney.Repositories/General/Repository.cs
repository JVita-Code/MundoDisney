using ApiMundoDisney.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }        

        public int Count(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Create(T entidad)
        {
            _context.Set<T>().Add(entidad);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> FindBy(ParametrosDeConsulta<T> parametrosDeQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Get(int id)
        {           
            return _context.Set<T>().Find(id);
        }

        public void Delete(T entidad)
        {
            _context.Set<T>().Remove(entidad);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entidad)
        {
            _context.Set<T>().Update(entidad);
        }        
    }
}
