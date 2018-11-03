using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AsyncPoco;
using Game.Infrastructure.PetaPoco.UnitOfWork;
using Game.Infrastructure.UnitOfWork;

namespace Game.Infrastructure.PetaPoco
{
    public class PetaPocoRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider provider;

        /// <summary>
        /// Gets the <see cref="IDatabase"/>.
        /// </summary>
        protected IDatabase Database => ((PetaPocoUnitOfWork)provider.GetUnitOfWorkInstance()).Database;

        public PetaPocoRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Database.SingleOrDefaultAsync<TEntity>(id);
        }

        public async Task<TEntity> GetAsync(int id, params string[] includes)
        {
            var entity = await GetAsync(id);
            var propertiesToLoad = typeof(TEntity).GetProperties()
                .Where(property => property.PropertyType.GetInterfaces().Contains(typeof(IEntity)) && includes.Contains(property.Name));
            foreach (var propertyToLoad in propertiesToLoad)
            {
                await SideLoadEntityAsync(entity, propertyToLoad);
            }
            return entity;
        }

        public Task<ICollection<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public void Create(TEntity entity)
        {
            var uow = provider.GetUnitOfWorkInstance() as PetaPocoUnitOfWork;
            uow.RegisterEntityToInsert(entity);
        }

        public void Update(TEntity entity)
        {
            var uow = provider.GetUnitOfWorkInstance() as PetaPocoUnitOfWork;
            uow.RegisterEntityToUpdate(entity);
        }

        public void Delete(int id)
        {
            var uow = provider.GetUnitOfWorkInstance() as PetaPocoUnitOfWork;
            uow.RegisterEntityToRemove<TEntity>(id);
        }

        private async Task SideLoadEntityAsync(TEntity entity, PropertyInfo propertyToLoad)
        {
            var foreignKeyValue = (int)(entity.GetType()
                .GetProperty(propertyToLoad.Name + nameof(IEntity.Id))
                ?.GetValue(entity) ?? null);
            var value = await Database.InvokeSingleOrDefaultAsync(propertyToLoad.PropertyType, foreignKeyValue);
            propertyToLoad.SetValue(entity, value);
        }
    }
}
