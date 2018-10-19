using DynamicRepository.Concrete;
using DynamicRepository.Contract;
using Game.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Infrastructure.Entity.Counter
{
    public class EntityCounter<TEntity> : EntityCounter<TEntity, GameDbContext>, IEntityCounter<TEntity> where TEntity : class, IIdentifiableEntity, new()
    {
        public EntityCounter(bool lazyLoadingEnabled, bool proxyCreationEnabled) : base(lazyLoadingEnabled, proxyCreationEnabled)
        { }
    }
}
