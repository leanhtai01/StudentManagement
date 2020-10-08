using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W3_CRUDOnDatabases_FactoryDP
{
    public partial class FormStudent : Form
    {
        string providerName;
        string connectionStringName;
        DbProviderFactory dbProvider;
        DbConnection dbConnection;

        /// <summary>
        /// establish a connection to database
        /// </summary>
        private void CreateConnection()
        {
            providerName = ConfigurationManager.AppSettings["ProviderName"];
            connectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"];
            dbProvider = DbProviderFactories.GetFactory(providerName);
            dbConnection = dbProvider.CreateConnection();
            dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        /// <summary>
        /// get data from database
        /// </summary>
        /// <param name="dbConn"></param>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private DataTable GetData(DbConnection dbConn, string command, params DbParameter[] parameters)
        {
            // declare variables
            DbDataAdapter dbDataAdapter = dbProvider.CreateDataAdapter();
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = dbProvider.CreateCommand();

            // add parameters to SqlCommand
            dbCommand.Connection = dbConn;
            dbCommand.CommandText = command;
            dbCommand.CommandType = CommandType.Text;
            dbCommand.Parameters.AddRange(parameters);

            // fill DataTable
            dbDataAdapter.SelectCommand = dbCommand;
            dbDataAdapter.Fill(dataTable);

            return dataTable;
        }
    }
}
