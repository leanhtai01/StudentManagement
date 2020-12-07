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
        BindingSource bindingSourceClass;
        BindingSource bindingSourceStudent;

        public StudentManagementView()
        {
            InitializeComponent();

            StudentManagementPresenter presenter = new StudentManagementPresenter(this);

            Load += (_, e) =>
            {
                LoadData?.Invoke(this, e);
                LoadClass();
            };
        }

        private void LoadClass()
        {
            comboBoxClass.DataSource = BindingSourceClass;
            comboBoxClass.DisplayMember = "TenLopHoc";
            textBoxClass.DataBindings.Add("Text", BindingSourceClass, "TenLopHoc");

            dataGridViewStudent.DataBindings.Add("DataSource", BindingSourceClass, "HocSinhs");
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

        public BindingSource BindingSourceClass { get => bindingSourceClass; set => bindingSourceClass = value; }

        public event EventHandler CreateClass;
        public event EventHandler UpdateClass;
        public event EventHandler DeleteClass;
        public event EventHandler CreateStudent;
        public event EventHandler UpdateStudent;
        public event EventHandler DeleteStudent;
        public event EventHandler LoadData;
    }
}
