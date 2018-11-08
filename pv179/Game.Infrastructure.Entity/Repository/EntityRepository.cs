using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure.UnitOfWork;
using System;

namespace Game.Infrastructure.Entity.Repository
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider _provider;

        protected DbContext Context => ((EntityUnitOfWork)_provider.GetUnitOfWorkInstance()).Context;

        public EntityRepository(IUnitOfWorkProvider provider)
        {
            this._provider = provider;
        }

        public Guid Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            Context.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public void Delete(Guid id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Guid id, params string[] includes)
        {
            DbQuery<TEntity> ctx = Context.Set<TEntity>();
            foreach (var include in includes)
            {
                ctx = ctx.Include(include);
            }
            return await ctx
                .SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            var foundEntity = Context.Set<TEntity>().Find(entity.Id);
            Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }
    }
}
