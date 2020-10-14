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

namespace StudentManagement
{
    public partial class FormStudent : Form
    {
        StudentBusinessLogic studentBL;
        ClassBusinessLogic classBL;

        public FormStudent()
        {
            studentBL = new StudentBusinessLogic();
            classBL = new ClassBusinessLogic();

            InitializeComponent();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            LoadClasses();

            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        } // end method FormStudent_Load

        public void LoadClasses()
        {
            comboBoxClass.DataSource = classBL.GetClassList();
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";
            textBoxClass.Text = comboBoxClass.Text;
        }

        public void LoadClasses(object value)
        {
            comboBoxClass.DataSource = classBL.GetClassList();
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";
            comboBoxClass.SelectedValue = value;
            textBoxClass.Text = comboBoxClass.Text;
        }

        /// <summary>
        /// re-fill data grid every time user change class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxClass_SelectedValueChanged(object sender, EventArgs e)
        {
            textBoxClass.Text = comboBoxClass.Text;
            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        } // end method ComboBoxClass_SelectedValueChanged

        /// <summary>
        /// fill data grid with list of students based on given class ID
        /// </summary>
        /// <param name="classId"></param>
        private void FillDataGridViewStudent(string classId)
        {
            dataGridViewStudent.DataSource = studentBL.GetStudentListByClassId(classId);
            dataGridViewStudent.Columns["HocSinhID"].HeaderText = "MSSV";
            dataGridViewStudent.Columns["TenHocSinh"].HeaderText = "Tên học sinh";
            dataGridViewStudent.Columns["NamSinh"].HeaderText = "Năm sinh";
            dataGridViewStudent.Columns["DiemTrungBinh"].HeaderText = "ĐTB";
            dataGridViewStudent.Columns["QueQuan"].HeaderText = "Quê quán";
            dataGridViewStudent.Columns["LopHocID"].HeaderText = "Mã lớp học";
            dataGridViewStudent.Columns["TenHocSinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns["QueQuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        } // end method FillDataGridViewStudent

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            FormAddStudent formAdd = new FormAddStudent();

            formAdd.StudentAdded += (mySender, myE) => FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
            formAdd.Show();
        } // end method ButtonAdd_Click

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudent.SelectedRows.Count > 0)
            {
                string studentId = dataGridViewStudent.SelectedRows[0].Cells["HocSinhID"].Value.ToString();

                studentBL.DeleteStudent(studentId);

                FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
            }
        } // end method buttonDelete_Click

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudent.SelectedRows.Count > 0)
            {
                string studentId = dataGridViewStudent.SelectedRows[0].Cells["HocSinhID"].Value.ToString();
                FormUpdateStudent formUpdate = new FormUpdateStudent(studentBL.GetStudent(studentId));

                formUpdate.StudentUpdated += (mySender, myE) => FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
                formUpdate.Show();
            }
        } // end method buttonUpdate_Click

        private void TextBoxClass_TextChanged(object sender, EventArgs e)
        {
            buttonUpdateClass.Enabled = true;

            if (string.IsNullOrEmpty(textBoxClass.Text.Trim()))
            {
                buttonUpdateClass.Enabled = false;
            }
        }

        private void ButtonUpdateClass_Click(object sender, EventArgs e)
        {
            string currentClassId = comboBoxClass.SelectedValue.ToString();
            Class c = classBL.GetClass(currentClassId);
            
            c.Name = textBoxClass.Text;
            classBL.UpdateClass(c);

            // reload class data
            LoadClasses(currentClassId);
        }

        private void buttonAddClass_Click(object sender, EventArgs e)
        {
            FormAddClass formAddClass = new FormAddClass();
            string currentClassId = comboBoxClass.SelectedValue.ToString();

            formAddClass.ClassAdded += (_, myE) => LoadClasses(currentClassId);
            formAddClass.Show();
        }
    }
}
