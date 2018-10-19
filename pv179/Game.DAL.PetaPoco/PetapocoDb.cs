using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.PetaPoco
{
    public class PetapocoDb
    {
        private const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=MyGameDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        private const string ProviderName = "System.Data.SqlClient";

        public IDatabase GetConnection() => new Database(ConnectionString, ProviderName);
    }
}
