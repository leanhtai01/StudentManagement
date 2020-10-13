using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
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

            // add parameters to DbCommand
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            // fill DataTable
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            return dataTable;
        } // end method GetData

        public object ExecuteScalar(string commandText, params DbParameter[] dbParameters)
        {
            DbCommand command = Provider.CreateCommand();
            object obj = null;

            // add parameters to DbCommand
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(dbParameters);

            Connection.Open();
            obj = command.ExecuteScalar();
            Connection.Close();

            return obj;
        } // end method ExecuteScalar

        public int ExecuteNonQuery(string commandText, DbParameter[] dbParameters)
        {
            DbCommand command = Provider.CreateCommand();
            int numOfRowsAffected;

            // add parameters to DbCommand
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(dbParameters);

            Connection.Open();
            numOfRowsAffected = command.ExecuteNonQuery();
            Connection.Close();

            return numOfRowsAffected;
        } // end method ExecuteNonQuery

        public DbDataReader ExecuteReader(string commandText, DbParameter[] dbParameters)
        {
            DbCommand command = Provider.CreateCommand();
            DbDataReader reader;

            // add parameters to DbCommand
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(dbParameters);

            Connection.Open();
            reader = command.ExecuteReader();

            return reader;
        }
    }
}
