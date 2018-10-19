using System.Collections.Generic;
using System.Threading.Tasks;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.UnitOfWork;
using Game.Infrastructure.Entity.UnitOfWork;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using Game.DAL.Entity;
using DynamicRepository.Concrete;

namespace Game.Infrastructure.Entity.Repository
{
    public class EntityRepository<TEntity> : EntityRepository<TEntity, GameDbContext>, IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider _provider;

        protected DbContext Context => ((EntityUnitOfWork)_provider.GetUnitOfWorkInstance()).Context;

        public Expression Expression => throw new NotImplementedException();

        public Type ElementType => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public EntityRepository(IUnitOfWorkProvider provider, bool lazyLoadingEnabled, bool proxyCreationEnabled)
            : base(lazyLoadingEnabled, proxyCreationEnabled)
        {
            this._provider = provider;
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(int id, params string[] includes)
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
