using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.Infrastructure.UnitOfWork;
using Game.Infrastructure.Entity.UnitOfWork;
using Game.Infrastructure;
using Game.Infrastructure.Query;
using Game.DAL.Entity.Config;
using System.Data.Entity;
using Game.DAL.Entity;
using Game.Infrastructure.Entity.Repository;
using Game.Infrastructure.Entity;
using Game.DAL.Entity.Entities;
using Game.DAL.Enums;
using Game.DAL.Entities;
using IntegracneTesty.Config;
using static IntegracneTesty.Config.Konstanty;
using BL.Config;

namespace IntegracneTesty.Tests.Config
{
    public class IntegracneTestyInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryEntityFrameworkTestDb";

        


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new BLInstaller().Install(container, store);

            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityQuery<>))
                    .LifestyleTransient()
            );
        }


        private static DbContext InitializeDatabase()
        {
            var context =  new GameDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));
            context.Accounts.RemoveRange(context.Accounts);
            context.Chat.RemoveRange(context.Chat);
            context.Characters.RemoveRange(context.Characters);
            context.Fights.RemoveRange(context.Fights);
            context.GroupPosts.RemoveRange(context.GroupPosts);
            context.Groups.RemoveRange(context.Groups);
            context.Messages.RemoveRange(context.Messages);
            context.Items.RemoveRange(context.Items);
            context.SaveChanges();




            context.Items.Add(itemSekera);
            context.Items.Add(itemMec);
            context.Items.Add(itemLuk);
            
            context.Characters.Add(characterSlayer);
            context.Characters.Add(characterWalker);
            context.Characters.Add(characterBela);

            context.Accounts.Add(accountPeter);
            context.Accounts.Add(accountIvan);
            context.Accounts.Add(accountVedro);

            context.Messages.Add(messageSlayerWalker);
            context.Messages.Add(messageWalkerSlayer);

            context.Fights.Add(fightSlayerWalker);

            context.Chat.Add(chatSlayerWalker);

            context.Groups.Add(groupPrvaSkupina);

            context.GroupPosts.Add(gPostPrvejSkupiny);

            context.SaveChanges();
            return context;
        }
    }
}
