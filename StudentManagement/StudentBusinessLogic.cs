using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_CRUDOnDatabases_FactoryDP
{
    public class StudentBusinessLogic
    {
        private DataAccess dataAccess;

        public StudentBusinessLogic()
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

        /// <summary>
        /// check whether student is exists
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool IsStudentExists(string studentId)
        {
            string commandText = "SELECT 1 FROM HocSinh WHERE HocSinhID = @studentId";
            List<DbParameter> listParameters = new List<DbParameter>();
            DbParameter parameter = dataAccess.Provider.CreateParameter();

            // add parameter
            parameter.ParameterName = "@studentID";
            parameter.Value = studentId;
            listParameters.Add(parameter);

            return dataAccess.ExecuteScalar(commandText, listParameters.ToArray()) != null;
        } // end method IsStudentExists

        /// <summary>
        /// insert a student to database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int InsertStudent(Student student)
        {
            string commandText =
                "INSERT INTO HocSinh (HocSinhID, TenHocSinh, NamSinh, DiemTrungBinh, QueQuan, LopHocID) "
                + "VALUES (@Id, @Name, @BirthYear, @GPA, @Hometown, @ClassId);";
            DbParameter[] listParameters = new DbParameter[6];

            // create parameters
            for (int i = 0; i < listParameters.Length; i++)
            {
                listParameters[i] = dataAccess.Provider.CreateParameter();
            }

            // add parameters
            listParameters[0].ParameterName = "@Id";
            listParameters[0].Value = student.Id;
            listParameters[1].ParameterName = "@Name";
            listParameters[1].Value = student.Name;
            listParameters[2].ParameterName = "@BirthYear";
            listParameters[2].Value = student.BirthYear;
            listParameters[3].ParameterName = "@GPA";
            listParameters[3].Value = student.GPA;
            listParameters[4].ParameterName = "@Hometown";
            listParameters[4].Value = student.Hometown;
            listParameters[5].ParameterName = "@ClassId";
            listParameters[5].Value = student.ClassId;

            return dataAccess.ExecuteNonQuery(commandText, listParameters);
        } // end method InsertStudent

        public int UpdateStudent(Student student)
        {
            string commandText =
                "UPDATE HocSinh "
                + "SET TenHocSinh = @Name, NamSinh = @BirthYear, DiemTrungBinh = @GPA, QueQuan = @Hometown, LopHocID = @ClassId "
                + "WHERE HocSinhID = @Id;";
            DbParameter[] listParameters = new DbParameter[6];

            // create parameters
            for (int i = 0; i < listParameters.Length; i++)
            {
                listParameters[i] = dataAccess.Provider.CreateParameter();
            }

            // add parameters
            listParameters[0].ParameterName = "@Id";
            listParameters[0].Value = student.Id;
            listParameters[1].ParameterName = "@Name";
            listParameters[1].Value = student.Name;
            listParameters[2].ParameterName = "@BirthYear";
            listParameters[2].Value = student.BirthYear;
            listParameters[3].ParameterName = "@GPA";
            listParameters[3].Value = student.GPA;
            listParameters[4].ParameterName = "@Hometown";
            listParameters[4].Value = student.Hometown;
            listParameters[5].ParameterName = "@ClassId";
            listParameters[5].Value = student.ClassId;

            return dataAccess.ExecuteNonQuery(commandText, listParameters);
        }

        public Student GetStudent(string studentId)
        {
            string commandText = "SELECT * FROM HocSinh WHERE HocSinhID = @studentId;";
            DbParameter[] listParameters = new DbParameter[1];
            Student student = new Student();
            DbDataReader reader;

            listParameters[0] = dataAccess.Provider.CreateParameter();
            listParameters[0].ParameterName = "@studentId";
            listParameters[0].Value = studentId;

            reader = dataAccess.ExecuteReader(commandText, listParameters);

            while (reader.Read())
            {
                student.Id = reader["HocSinhID"].ToString();
                student.Name = reader["TenHocSinh"].ToString();
                student.BirthYear = int.Parse(reader["NamSinh"].ToString());
                student.GPA = double.Parse(reader["DiemTrungBinh"].ToString());
                student.Hometown = reader["QueQuan"].ToString();
                student.ClassId = reader["LopHocID"].ToString();
            }

            dataAccess.Connection.Close();

            return student;
        }

        /// <summary>
        /// delete a student from database
        /// </summary>
        /// <param name="studentId"></param>
        public int DeleteStudent(string studentId)
        {
            string commandText = "DELETE FROM HocSinh WHERE HocSinhID = @studentId;";
            List<DbParameter> listParameters = new List<DbParameter>();
            DbParameter parameter = dataAccess.Provider.CreateParameter();

            // add parameter
            parameter.ParameterName = "@studentId";
            parameter.Value = studentId;
            listParameters.Add(parameter);

            return dataAccess.ExecuteNonQuery(commandText, listParameters.ToArray());
        } // end method DeleteStudent
    }
}
