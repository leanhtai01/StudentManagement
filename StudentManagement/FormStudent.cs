using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
        DbDataAdapter dataAdapterStudent;
        DbDataAdapter dataAdapterClass;
        BindingSource bindingSourceStudent = new BindingSource();
        BindingSource bindingSourceClass = new BindingSource();
        string tableNameStudent = "HocSinh";
        string tableNameClass = "LopHoc";

        public FormStudent()
        {
            InitializeComponent();

            buttonAddStudent.Click += ButtonAddStudent_Click;
            buttonDeleteStudent.Click += ButtonDeleteStudent_Click;
            buttonUpdateStudent.Click += ButtonUpdateStudent_Click;
            buttonUpdateClass.Click += ButtonUpdateClass_Click;
        }

        private void ButtonUpdateClass_Click(object sender, EventArgs e)
        {
            BindingContext[bindingSourceClass].EndCurrentEdit();
            dataAdapterClass.Update(dataSet, tableNameClass);
        }

        private void ButtonUpdateStudent_Click(object sender, EventArgs e)
        {
            Student student = ConvertDataRowViewToStudent((DataRowView)BindingContext[bindingSourceStudent].Current);
            FormUpdateStudent formUpdateStudent = new FormUpdateStudent(dataSet, dataAdapterStudent, student);

            formUpdateStudent.Show();
        }

        private Student ConvertDataRowViewToStudent(DataRowView dataRowView)
        {
            return new Student
            {
                Id = dataRowView["HocSinhId"].ToString(),
                Name = dataRowView["TenHocSinh"].ToString(),
                BirthYear = int.Parse(dataRowView["NamSinh"].ToString()),
                GPA = double.Parse(dataRowView["DiemTrungBinh"].ToString()),
                Hometown = dataRowView["QueQuan"].ToString(),
                ClassId = dataRowView["LopHocID"].ToString()
            };
        }

        private void ButtonDeleteStudent_Click(object sender, EventArgs e)
        {
            if (bindingSourceStudent.Count != 0)
            {
                bindingSourceStudent.RemoveAt(BindingContext[bindingSourceStudent].Position);
                dataAdapterStudent.Update(dataSet, tableNameStudent);
            }
        }

        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            FormAddStudent formAddStudent = new FormAddStudent(dataSet, dataAdapterStudent);

            formAddStudent.Show();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            LoadComboBoxDatabase();
        } // end method FormStudent_Load

        private void LoadData()
        {
            string relNameStudentClass = "StudentClass";
            DataRelation relStudentClass;
            DataColumn parentColumn;
            DataColumn childColumn;

            // load and add data, relation to dataset
            dataAdapterStudent = studentBL.GetStudentList(dataSet, tableNameStudent);
            dataAdapterClass = classBL.GetClassList(dataSet, tableNameClass);
            parentColumn = dataSet.Tables[tableNameClass].Columns["LopHocID"];
            childColumn = dataSet.Tables[tableNameStudent].Columns["LopHocID"];
            relStudentClass = new DataRelation(relNameStudentClass, parentColumn, childColumn);
            dataSet.Relations.Add(relStudentClass);

            // set primary key for tables in dataset
            dataSet.Tables[tableNameStudent].PrimaryKey = new DataColumn[] { dataSet.Tables[tableNameStudent].Columns["HocSinhID"] };
            dataSet.Tables[tableNameClass].PrimaryKey = new DataColumn[] { dataSet.Tables[tableNameClass].Columns["LopHocID"] };

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
