using System.Collections.Generic;
using System.Threading.Tasks;
using Game.DAL.Entity.Entities;

namespace Game.Infrastructure.Entity.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
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
