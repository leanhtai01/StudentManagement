using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class ClassBusinessLogic
    {
        private DataAccess dataAccess;

        public ClassBusinessLogic()
        {
            dataAccess = new DataAccess();
        }

        /// <summary>
        /// get class list
        /// </summary>
        /// <returns></returns>
        public DataTable GetClassList()
        {
            string commandText = "SELECT * FROM LopHoc;";

            return dataAccess.GetDataTable(commandText);
        } // end method GetClassList
    }
}
