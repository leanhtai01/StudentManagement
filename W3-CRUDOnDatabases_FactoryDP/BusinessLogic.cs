using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_CRUDOnDatabases_FactoryDP
{
    public class BusinessLogic
    {
        private DataAccess dataAccess;

        public BusinessLogic()
        {
            dataAccess = new DataAccess();
        }

        /// <summary>
        /// get student list based on given class id
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public DataTable GetStudentListByClassId(string classId)
        {
            string commandText = "SELECT * FROM HocSinh WHERE LopHocID = @classID;";
            List<DbParameter> listParameters = new List<DbParameter>();
            DbParameter parameter = dataAccess.Provider.CreateParameter();

            // add parameter
            parameter.ParameterName = "@classID";
            parameter.Value = classId;
            listParameters.Add(parameter);

            return dataAccess.GetDataTable(commandText, listParameters.ToArray());
        } // end method GetStudentListByClassId

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
