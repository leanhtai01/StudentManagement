using System.Configuration;
using System.Data;
using System.Data.Common;

namespace StudentManagement
{
    public class DataAccess
    {
        private string providerName = ConfigurationManager.AppSettings["ProviderName"];
        private string connectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"];

        public DbProviderFactory Provider { get; set; }
        public DbConnection Connection { get; set; }

        public DataAccess(string providerName, string connectionStringName)
        {
            Provider = DbProviderFactories.GetFactory(providerName);
            Connection = Provider.CreateConnection();
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public DbDataAdapter GetDataTable(DataSet dataSet, string tableName, string commandText, params DbParameter[] parameters)
        {
            // declare variables
            DbDataAdapter dataAdapter = Provider.CreateDataAdapter();
            DbCommandBuilder commandBuilder = Provider.CreateCommandBuilder();
            DataTable dataTable = new DataTable(tableName);
            DbCommand command = Provider.CreateCommand();

            // add parameters to DbCommand
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            // fill DataTable
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet, tableName);

            commandBuilder.DataAdapter = dataAdapter;

            return dataAdapter;
        } // end method GetData
    }
}
