using StudentManagement.Model;
using StudentManagement.Presenter;
using StudentManagement.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class StudentManagementView : Form, IStudentManagementView
    {
        public StudentManagementView()
        {
            InitializeComponent();

            StudentManagementPresenter presenter = new StudentManagementPresenter(this);

            Load += (_, e) =>
            {
                LoadData?.Invoke(this, e);
                LoadClass();
            };

            buttonUpdateClass.Click += (_, e) =>
            {
                UpdateClass?.Invoke(buttonUpdateClass, e);
                MessageBox.Show("Cập nhật lớp học thành công!");
            };

            buttonDeleteClass.Click += (_, e) =>
            {
                DeleteClass?.Invoke(buttonDeleteClass, e);
                MessageBox.Show("Xóa lớp học thành công!");
            };

            buttonAddClass.Click += (_, e) =>
            {
                FormAddClass formAddClass = new FormAddClass(BindingSourceClass, Db);

                formAddClass.Show();
            };

            buttonAddStudent.Click += (_, e) =>
            {
                FormAddStudent formAddStudent = new FormAddStudent(BindingSourceClass, BindingSourceStudent, Db);

                formAddStudent.Show();
            };

            buttonDeleteStudent.Click += (_, e) =>
            {
                DeleteStudent?.Invoke(buttonDeleteStudent, e);
            };

            buttonUpdateStudent.Click += (_, e) =>
            {
                FormUpdateStudent formUpdateStudent = new FormUpdateStudent(BindingSourceClass, BindingSourceStudent, Db);

                formUpdateStudent.Show();
            };
        }

        private void LoadClass()
        {
            comboBoxClass.DataSource = BindingSourceClass;
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";
            textBoxClass.DataBindings.Add("Text", BindingSourceClass, "TenLopHoc");

            dataGridViewStudent.DataSource = BindingSourceStudent;
            InitializeDataGridViewStudent();
        }

        private void InitializeDataGridViewStudent()
        {
            dataGridViewStudent.Columns["LopHoc"].Visible = false;
            dataGridViewStudent.Columns["HocSinhID"].HeaderText = "MSSV";
            dataGridViewStudent.Columns["TenHocSinh"].HeaderText = "Tên học sinh";
            dataGridViewStudent.Columns["NamSinh"].HeaderText = "Năm sinh";
            dataGridViewStudent.Columns["DiemTrungBinh"].HeaderText = "ĐTB";
            dataGridViewStudent.Columns["QueQuan"].HeaderText = "Quê quán";
            dataGridViewStudent.Columns["LopHocID"].HeaderText = "Mã lớp học";
            dataGridViewStudent.Columns["TenHocSinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns["QueQuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public BindingSource BindingSourceClass { get; set; }
        public BindingSource BindingSourceStudent { get; set; }
        public QLHSDataContext Db { get; set; }

        public event EventHandler UpdateClass;
        public event EventHandler DeleteClass;
        public event EventHandler DeleteStudent;
        public event EventHandler LoadData;
    }
}
