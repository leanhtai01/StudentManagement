using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W3_CRUDOnDatabases_FactoryDP
{
    public partial class FormStudent : Form
    {
        public FormStudent()
        {
            InitializeComponent();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            // declare variable
            string command = "SELECT * FROM LopHoc;";

            CreateConnection();

            comboBoxClass.DataSource = GetData(dbConnection, command);
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";

            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        }

        private void ComboBoxClass_SelectedValueChanged(object sender, EventArgs e)
        {
            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        }

        private void FillDataGridViewStudent(string classID)
        {
            // declare variables
            string command = "SELECT * FROM HocSinh WHERE LopHocID = @classID;";
            List<DbParameter> listParameters = new List<DbParameter>();
            DbParameter parameter = dbProvider.CreateParameter();

            // add parameter
            parameter.ParameterName = "@classID";
            parameter.Value = classID;
            listParameters.Add(parameter);

            dataGridViewStudent.DataSource = GetData(dbConnection, command, listParameters.ToArray());
            dataGridViewStudent.Columns["HocSinhID"].HeaderText = "MSSV";
            dataGridViewStudent.Columns["TenHocSinh"].HeaderText = "Tên học sinh";
            dataGridViewStudent.Columns["NamSinh"].HeaderText = "Năm sinh";
            dataGridViewStudent.Columns["DiemTrungBinh"].HeaderText = "ĐTB";
            dataGridViewStudent.Columns["QueQuan"].HeaderText = "Quê quán";
            dataGridViewStudent.Columns["LopHocID"].HeaderText = "Mã lớp học";
            dataGridViewStudent.Columns["TenHocSinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns["QueQuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
