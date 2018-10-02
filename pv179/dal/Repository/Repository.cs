using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
    {

        public void Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(int id, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
