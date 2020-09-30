using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W2_RetrieveDataFromSQLServer
{
    public partial class FormStudent : Form
    {
        string connStr = ConfigurationManager.ConnectionStrings["SqlServerConnStr"].ConnectionString;
        SqlConnection sqlConn;

        public FormStudent()
        {
            InitializeComponent();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            // declare variable
            string command = "SELECT * FROM LopHoc;";

            sqlConn = new SqlConnection(connStr);

            comboBoxClass.DataSource = GetData(sqlConn, command);
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";

            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        }

        private void comboBoxClass_SelectedValueChanged(object sender, EventArgs e)
        {
            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        }

        private void FillDataGridViewStudent(string id)
        {
            // declare variables
            string command = "SELECT * FROM HocSinh WHERE LopHocID = @id;";
            List<SqlParameter> parameters = new List<SqlParameter>();

            // add parameter
            parameters.Add(new SqlParameter("@id", id));

            dataGridViewStudent.DataSource = GetData(sqlConn, command, parameters.ToArray());
            dataGridViewStudent.Columns["HocSinhID"].HeaderText = "MSSV";
            dataGridViewStudent.Columns["TenHocSinh"].HeaderText = "Tên học sinh";
            dataGridViewStudent.Columns["NamSinh"].HeaderText = "Năm sinh";
            dataGridViewStudent.Columns["DiemTrungBinh"].HeaderText = "ĐTB";
            dataGridViewStudent.Columns["QueQuan"].HeaderText = "Quê quán";
            dataGridViewStudent.Columns["LopHocID"].HeaderText = "Mã lớp học";
            dataGridViewStudent.Columns["TenHocSinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns["QueQuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private DataTable GetData(SqlConnection sqlConn, string command, params SqlParameter[] parameters)
        {
            // declare variables
            SqlDataAdapter sqlDataAdapter;
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConn);

            // add parameters to SqlCommand
            sqlCommand.Parameters.AddRange(parameters);

            // fill DataTable
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }
    }
}
