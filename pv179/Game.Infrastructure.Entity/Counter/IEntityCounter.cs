using DynamicRepository.Contract;
using Game.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Infrastructure.Entity.Counter
{
    public interface IEntityCounter<TEntity> : IEntityCounter<TEntity, GameDbContext> where TEntity : class, IIdentifiableEntity, new()
    { }
}
