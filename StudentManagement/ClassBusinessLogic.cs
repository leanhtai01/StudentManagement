using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class ClassBusinessLogic
    {
        private DataAccess dataAccess;

        public ClassBusinessLogic(string providerName, string connectionStringName)
        {
            dataAccess = new DataAccess(providerName, connectionStringName);
        }

        public DbDataAdapter GetClassList(DataSet dataSet, string tableName)
        {
            string commandText = "SELECT * FROM LopHoc;";

            return dataAccess.GetDataTable(dataSet, tableName, commandText);
        } // end method GetClassList
    }
}
