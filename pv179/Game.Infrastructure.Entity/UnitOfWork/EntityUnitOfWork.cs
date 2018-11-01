using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Game.Infrastructure.UnitOfWork;

namespace Game.Infrastructure.Entity.UnitOfWork
{
    public class EntityUnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        public DbContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityUnitOfWork"/> class.
        /// </summary>
        public EntityUnitOfWork(Func<DbContext> dbContextFactory)
        {
            this.Context = dbContextFactory?.Invoke() ?? throw new ArgumentException("Db context factory cant be null!");
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        protected override async Task CommitCore()
        {
            await Context.SaveChangesAsync();
        }

        public override void Dispose()
        {
            Context.Dispose();
        }
    }
}
