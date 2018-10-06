using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IList<Action> afterCommitActions = new List<Action>();

        public GameDbContext Context { get; }

        public UnitOfWork(Func<GameDbContext> dbContextFactory)
        {
            this.Context = dbContextFactory?.Invoke() ?? throw new ArgumentException("Db context factory cant be null!");
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
            foreach (var action in afterCommitActions)
            {
                action();
            }
            afterCommitActions.Clear();
        }

        public void RegisterAction(Action action)
        {
            afterCommitActions.Add(action);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
