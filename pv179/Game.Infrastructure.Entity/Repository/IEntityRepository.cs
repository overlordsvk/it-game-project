using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicRepository.Contract;
using Game.DAL.Entity;
using Game.DAL.Entity.Entities;
using DynamicRepository.Concrete;

namespace Game.Infrastructure.Entity.Repository
{
    public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, GameDbContext> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Gets the entity with given id.
        /// </summary>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Gets the entity with given id.
        /// </summary>
        Task<TEntity> GetAsync(int id, params string[] includes);

        Task<ICollection<TEntity>> GetAllAsync();

        /// <summary>
        /// Persists the given entity.
        /// </summary>
        void Create(TEntity entity);

        /// <summary>
        /// Updates the given entity.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity with the given id.
        /// </summary>
        void Delete(int id);
    }
}
