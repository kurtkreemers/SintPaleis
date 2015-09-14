using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class DBmanager
    {
        private static ConnectionStringSettings conAccessSetting = ConfigurationManager.ConnectionStrings["Access"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conAccessSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var conAccess = factory.CreateConnection();
            conAccess.ConnectionString = conAccessSetting.ConnectionString;
            return conAccess;
        }
    }
}
