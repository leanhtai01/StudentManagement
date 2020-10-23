using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class FormStudent : Form
    {
        StudentBusinessLogic studentBL;
        ClassBusinessLogic classBL;
        string providerName;
        string connectionStringName;
        DataSet dataSet = new DataSet();
        BindingSource bindingSourceStudent = new BindingSource();
        BindingSource bindingSourceClass = new BindingSource();

        public FormStudent()
        {
            InitializeComponent();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            LoadComboBoxDatabase();
        } // end method FormStudent_Load

        private void LoadData()
        {
            string tableNameStudent = "HocSinh";
            string tableNameClass = "LopHoc";
            string relNameStudentClass = "StudentClass";
            DataRelation relStudentClass;
            DataColumn parentColumn;
            DataColumn childColumn;

            // load and add data, relation to dataset
            dataSet.Tables.Add(studentBL.GetStudentList(tableNameStudent));
            dataSet.Tables.Add(classBL.GetClassList(tableNameClass));
            parentColumn = dataSet.Tables[tableNameClass].Columns["LopHocID"];
            childColumn = dataSet.Tables[tableNameStudent].Columns["LopHocID"];
            relStudentClass = new DataRelation(relNameStudentClass, parentColumn, childColumn);
            dataSet.Relations.Add(relStudentClass);

            // binding data to combobox
            bindingSourceClass = new BindingSource(dataSet, tableNameClass);
            comboBoxClass.DataSource = bindingSourceClass;
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";

            // binding data to textBoxClass
            textBoxClass.DataBindings.Add("Text", bindingSourceClass, "TenLopHoc");

            // binding data to dataGridViewStudent
            bindingSourceStudent = new BindingSource(bindingSourceClass, relNameStudentClass);
            dataGridViewStudent.DataSource = bindingSourceStudent;
            MakeCustomDataGridViewStudent();
        }

        public void LoadComboBoxDatabase()
        {
            Dictionary<string, string> databases = new Dictionary<string, string>
            {
                {"SQL Server", "SQLServerConnectionString"},
                {"MySQL Server", "MySQLConnectionString" }
            };

            comboBoxDatabase.DataSource = new BindingSource(databases, null);
            comboBoxDatabase.DisplayMember = "Key";
            comboBoxDatabase.ValueMember = "Value";
            comboBoxDatabase.SelectedValue = "SQLServerConnectionString";
        }

        private void MakeCustomDataGridViewStudent()
        {
            dataGridViewStudent.Columns["HocSinhID"].HeaderText = "MSSV";
            dataGridViewStudent.Columns["TenHocSinh"].HeaderText = "Tên học sinh";
            dataGridViewStudent.Columns["NamSinh"].HeaderText = "Năm sinh";
            dataGridViewStudent.Columns["DiemTrungBinh"].HeaderText = "ĐTB";
            dataGridViewStudent.Columns["QueQuan"].HeaderText = "Quê quán";
            dataGridViewStudent.Columns["LopHocID"].HeaderText = "Mã lớp học";
            dataGridViewStudent.Columns["TenHocSinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns["QueQuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void TextBoxClass_TextChanged(object sender, EventArgs e)
        {
            buttonUpdateClass.Enabled = true;

            if (string.IsNullOrEmpty(textBoxClass.Text.Trim()))
            {
                buttonUpdateClass.Enabled = false;
            }
        }

        private void ComboBoxDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            connectionStringName = ((KeyValuePair<string, string>)comboBoxDatabase.SelectedItem).Value.ToString();
            providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

            studentBL = new StudentBusinessLogic(providerName, connectionStringName);
            classBL = new ClassBusinessLogic(providerName, connectionStringName);

            textBoxClass.DataBindings.Clear();
            dataSet.Reset();
            LoadData();
        }
    }
}
