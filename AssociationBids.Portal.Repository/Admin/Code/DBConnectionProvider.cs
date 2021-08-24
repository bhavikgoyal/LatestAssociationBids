using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Admin.Code
{
    public class DBConnectionProvider
    {
        /// <summary>
        /// The database connection.
        /// </summary>
        DbConnection dbConnection;

        /// <summary>
        /// The database connection string
        /// </summary>
        readonly string dbConnectionString;

        /// <summary>
        /// The database factory
        /// </summary>
        DbProviderFactory dbFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBConnectionProvider"/> class.
        /// </summary>
        public DBConnectionProvider()
        {
            dbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            dbFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DBConnectionProvider"/> class.
        /// </summary>
        /// <param name="dbConnectionString">The database connection string.</param>
        public DBConnectionProvider(string dbConnectionString)
        {
            this.dbConnectionString = ConfigurationManager.ConnectionStrings[dbConnectionString].ConnectionString;
            dbFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings[dbConnectionString].ProviderName);
        }

        /// <summary>
        /// Gets the open connection.
        /// </summary>
        /// <returns>Database Connection.</returns>
        public DbConnection GetOpenConnection()
        {
            dbConnection = dbFactory.CreateConnection();
            dbConnection.ConnectionString = dbConnectionString;
            dbConnection.Open();

            return dbConnection;
        }
    }
}
