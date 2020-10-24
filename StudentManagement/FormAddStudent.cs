using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class FormAddStudent : Form
    {
        StudentBusinessLogic studentBL;
        ClassBusinessLogic classBL;
        DataSet dataSet;
        DbDataAdapter dataAdapter;
        string tableNameStudent = "HocSinh";
        string tableNameClass = "LopHoc";
        BindingSource bindingSourceClass;
        public delegate void StudentAddedEventHandler(object sender, EventArgs e);
        public event StudentAddedEventHandler StudentAdded;

        public FormAddStudent(string providerName, string connectionStringName)
        {
            studentBL = new StudentBusinessLogic(providerName, connectionStringName);
            classBL = new ClassBusinessLogic(providerName, connectionStringName);

            InitializeComponent();
        }

        public FormAddStudent(DataSet dataSet, DbDataAdapter dataAdapter)
        {
            InitializeComponent();

            this.dataSet = dataSet;
            this.dataAdapter = dataAdapter;
        }

        public FormAddStudent(DataSet dataSet, DbDataAdapter dataAdapter, BindingSource bindingSourceClass)
        {
            InitializeComponent();

            this.dataSet = dataSet;
            this.dataAdapter = dataAdapter;
            this.bindingSourceClass = bindingSourceClass;
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            buttonAdd.Enabled = false;

            comboBoxClass.DataSource = new BindingSource(dataSet, tableNameClass);
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";
            comboBoxClass.SelectedValue = (BindingContext[bindingSourceClass].Current as DataRowView)["LopHocID"].ToString();
        }

        /// <summary>
        /// make sure user fill all information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            buttonAdd.Enabled = true;

            foreach (TextBox textBox in tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(textBox.Text.Trim()))
                {
                    buttonAdd.Enabled = false;
                    break;
                }
            }
        } // end method TextBox_TextChanged

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
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

        /// <summary>
        /// // only allow non-negative integers in textBoxBirthYear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxBirthYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// only allow non-negative real number in textBoxGPA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxGPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        } // end method TextBoxGPA_KeyPress

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            DataTable dataTableStudent = dataSet.Tables[tableNameStudent];
            DataRow tmpRow = dataTableStudent.Rows.Find(textBoxId.Text.Trim());

            if (tmpRow == null)
            {
                Student newStudent = GetStudent();
                DataRow newRow = dataTableStudent.NewRow();

                newRow["HocSinhID"] = newStudent.Id;
                newRow["TenHocSinh"] = newStudent.Name;
                newRow["NamSinh"] = newStudent.BirthYear;
                newRow["DiemTrungBinh"] = newStudent.GPA;
                newRow["QueQuan"] = newStudent.Hometown;
                newRow["LopHocID"] = newStudent.ClassId;
                dataTableStudent.Rows.Add(newRow);

                dataAdapter.Update(dataSet, tableNameStudent);

                MessageBox.Show("Thêm học sinh thành công!");
            }
            else
            {
                MessageBox.Show("Học sinh đã tồn tại! Không thể thêm!");
            }
        } // end method ButtonAdd_Click
    }
}
