﻿using System;
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

        public int UpdateClass(Class c)
        {
            string commandText =
                "UPDATE LopHoc "
                + "SET TenLopHoc = @TenLopHoc "
                + "WHERE LopHocID = @LopHocId;";
            DbParameter[] listParameters = new DbParameter[2];

            // create parameters
            for (int i = 0; i < listParameters.Length; i++)
            {
                listParameters[i] = dataAccess.Provider.CreateParameter();
            }

            // add parameters
            listParameters[0].ParameterName = "@LopHocID";
            listParameters[0].Value = c.Id;
            listParameters[1].ParameterName = "@TenLopHoc";
            listParameters[1].Value = c.Name;

            return dataAccess.ExecuteNonQuery(commandText, listParameters);
        }

        public Class GetClass(string classId)
        {
            string commandText = "SELECT * FROM LopHoc WHERE LopHocID = @classId;";
            DbParameter[] listParameters = new DbParameter[1];
            Class c = new Class();
            DbDataReader reader;

            listParameters[0] = dataAccess.Provider.CreateParameter();
            listParameters[0].ParameterName = "@classId";
            listParameters[0].Value = classId;

            reader = dataAccess.ExecuteReader(commandText, listParameters);

            while (reader.Read())
            {
                c.Id = reader["LopHocID"].ToString();
                c.Name = reader["TenLopHoc"].ToString();
            }

            dataAccess.Connection.Close();

            return c;
        }
    }
}
