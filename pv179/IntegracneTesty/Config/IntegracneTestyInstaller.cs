using BL.Config;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Game.DAL.Entity;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using static IntegracneTesty.Config.Konstanty;

namespace IntegracneTesty.Tests.Config
{
    public class IntegracneTestyInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryEntityFrameworkTestDb";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new BLInstaller().Install(container, store);
        }

        private static DbContext InitializeDatabase()
        {
            var context = new GameDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));
            //context.Accounts.RemoveRange(context.Accounts);
            //context.Chat.RemoveRange(context.Chat);
            //context.Characters.RemoveRange(context.Characters);
            //context.Fights.RemoveRange(context.Fights);
            //context.GroupPosts.RemoveRange(context.GroupPosts);
            //context.Groups.RemoveRange(context.Groups);
            //context.Messages.RemoveRange(context.Messages);
            //context.Items.RemoveRange(context.Items);
            //context.SaveChanges();

            context.Items.AddOrUpdate(itemSekera);
            context.Items.AddOrUpdate(itemMec);
            context.Items.AddOrUpdate(itemLuk);

            context.Characters.AddOrUpdate(characterSlayer);
            context.Characters.AddOrUpdate(characterWalker);
            context.Characters.AddOrUpdate(characterBela);

            context.Accounts.AddOrUpdate(accountPeter);
            context.Accounts.AddOrUpdate(accountIvan);
            context.Accounts.AddOrUpdate(accountVedro);

            context.Messages.AddOrUpdate(messageSlayerWalker);
            context.Messages.AddOrUpdate(messageWalkerSlayer);

            context.Fights.AddOrUpdate(fightSlayerWalker);

            context.Chat.AddOrUpdate(chatSlayerWalker);

            context.Groups.AddOrUpdate(groupPrvaSkupina);

            context.GroupPosts.AddOrUpdate(gPostPrvejSkupiny);

            context.SaveChanges();
            return context;
        }
    }
}