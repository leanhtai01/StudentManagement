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
        StudentBusinessLogic businessLogic;

        public FormStudent()
        {
            businessLogic = new StudentBusinessLogic();

            InitializeComponent();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            comboBoxClass.DataSource = businessLogic.GetClassList();
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";

            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        } // end method FormStudent_Load

        /// <summary>
        /// re-fill data grid every time user change class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxClass_SelectedValueChanged(object sender, EventArgs e)
        {
            FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
        } // end method ComboBoxClass_SelectedValueChanged

        /// <summary>
        /// fill data grid with list of students based on given class ID
        /// </summary>
        /// <param name="classId"></param>
        private void FillDataGridViewStudent(string classId)
        {
            dataGridViewStudent.DataSource = businessLogic.GetStudentListByClassId(classId);
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
            FormAdd formAdd = new FormAdd();

            formAdd.StudentAdded += (mySender, myE) => FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
            formAdd.Show();
        } // end method ButtonAdd_Click

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudent.SelectedRows.Count > 0)
            {
                string studentId = dataGridViewStudent.SelectedRows[0].Cells["HocSinhID"].Value.ToString();

                businessLogic.DeleteStudent(studentId);

                FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
            }
        } // end method buttonDelete_Click

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudent.SelectedRows.Count > 0)
            {
                string studentId = dataGridViewStudent.SelectedRows[0].Cells["HocSinhID"].Value.ToString();
                FormUpdate formUpdate = new FormUpdate(businessLogic.GetStudent(studentId));

                formUpdate.StudentUpdated += (mySender, myE) => FillDataGridViewStudent(comboBoxClass.SelectedValue.ToString());
                formUpdate.Show();
            }
        } // end method buttonUpdate_Click
    }
}
