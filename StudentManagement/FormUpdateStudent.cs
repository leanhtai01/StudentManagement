using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class FormUpdateStudent : Form
    {
        StudentBusinessLogic studentBL;
        ClassBusinessLogic classBL;
        Student student;
        public delegate void StudentUpdatedEventHandler(object sender, EventArgs e);
        public event StudentUpdatedEventHandler StudentUpdated;
        DataSet dataSet;
        DbDataAdapter dataAdapter;
        string tableNameStudent = "HocSinh";
        string tableNameClass = "LopHoc";

        public FormUpdateStudent(string providerName, string connectionStringName, Student student)
        {
            studentBL = new StudentBusinessLogic(providerName, connectionStringName);
            classBL = new ClassBusinessLogic(providerName, connectionStringName);
            this.student = student;

            InitializeComponent();
        }

        public FormUpdateStudent(DataSet dataSet, DbDataAdapter dataAdapter, Student student)
        {
            InitializeComponent();

            this.dataSet = dataSet;
            this.dataAdapter = dataAdapter;
            this.student = student;
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;

            comboBoxClass.DataSource = new BindingSource(dataSet, tableNameClass);
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";

            textBoxId.Text = student.Id;
            textBoxName.Text = student.Name;
            textBoxBirthYear.Text = student.BirthYear.ToString();
            textBoxGPA.Text = student.GPA.ToString();
            textBoxHometown.Text = student.Hometown;
            comboBoxClass.SelectedValue = student.ClassId;
        }

        /// <summary>
        /// make sure user fill all information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;

            foreach (TextBox textBox in tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(textBox.Text.Trim()))
                {
                    buttonUpdate.Enabled = false;
                    break;
                }
            }
        } // end method TextBox_TextChanged

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxBirthYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxGPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            Student updatedStudent = GetStudent();
            DataTable dataTableStudent = dataSet.Tables[tableNameStudent];
            DataRow updatedRow = dataTableStudent.Rows.Find(updatedStudent.Id);

            updatedRow["TenHocSinh"] = updatedStudent.Name;
            updatedRow["NamSinh"] = updatedStudent.BirthYear;
            updatedRow["DiemTrungBinh"] = updatedStudent.GPA;
            updatedRow["QueQuan"] = updatedStudent.Hometown;
            updatedRow["LopHocID"] = updatedStudent.ClassId;

            dataAdapter.Update(dataSet, tableNameStudent);

            MessageBox.Show("Cập nhật học sinh thành công!");
        }

        /// <summary>
        /// create a Student using information from UI
        /// </summary>
        /// <returns></returns>
        private Student GetStudent()
        {
            return new Student
            {
                Id = textBoxId.Text.Trim(),
                Name = textBoxName.Text.Trim(),
                BirthYear = Int32.Parse(textBoxBirthYear.Text),
                GPA = Double.Parse(textBoxGPA.Text),
                Hometown = textBoxHometown.Text.Trim(),
                ClassId = comboBoxClass.SelectedValue.ToString()
            };
        }
    }
}
