using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_CRUDOnDatabases_FactoryDP
{
    public class DataAccess
    {
        private string providerName = ConfigurationManager.AppSettings["ProviderName"];
        private string connectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"];

        public DbProviderFactory Provider { get; set; }
        public DbConnection Connection { get; set; }

        public DataAccess()
        {
            Provider = DbProviderFactories.GetFactory(providerName);
            Connection = Provider.CreateConnection();
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        /// <summary>
        /// get data from database
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string commandText, params DbParameter[] parameters)
        {
            // declare variables
            DbDataAdapter dataAdapter = Provider.CreateDataAdapter();
            DataTable dataTable = new DataTable();
            DbCommand command = Provider.CreateCommand();

            // add parameters to SqlCommand
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            // fill DataTable
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            return dataTable;
        } // end method GetData
    }
}
