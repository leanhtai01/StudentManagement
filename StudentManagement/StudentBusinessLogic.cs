using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class StudentBusinessLogic
    {
        private DataAccess dataAccess;

        public StudentBusinessLogic(string providerName, string connectionStringName)
        {
            dataAccess = new DataAccess(providerName, connectionStringName);
        }

        public DbDataAdapter GetStudentList(DataSet dataSet, string tableName)
        {
            string commandText = "SELECT * FROM HocSinh";

            return dataAccess.GetDataTable(dataSet, tableName, commandText);
        } // end method GetStudentList
    }
}
