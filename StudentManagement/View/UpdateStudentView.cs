using StudentManagement.Model;
using StudentManagement.Presenter;
using StudentManagement.View;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class FormUpdateStudent : Form, IUpdateStudentView
    {
        public FormUpdateStudent(BindingSource bindingSourceClass, BindingSource bindingSourceStudent, QLHSDataContext db)
        {
            InitializeComponent();
            UpdateStudentPresenter presenter = new UpdateStudentPresenter(this);
            BindingSourceClass = bindingSourceClass;
            BindingSourceStudent = bindingSourceStudent;
            Db = db;
            UpdatedStudent = (HocSinh)BindingSourceStudent.Current;

            SetupComboBoxClass();
            buttonUpdate.Enabled = true;
            textBoxName.TextChanged += TextBox_TextChanged;
            textBoxBirthYear.TextChanged += TextBox_TextChanged;
            textBoxGPA.TextChanged += TextBox_TextChanged;
            textBoxHometown.TextChanged += TextBox_TextChanged;

            textBoxId.Text = UpdatedStudent.HocSinhID;
            textBoxName.Text = UpdatedStudent.TenHocSinh;
            textBoxBirthYear.Text = UpdatedStudent.NamSinh.ToString();
            textBoxGPA.Text = UpdatedStudent.DiemTrungBinh.ToString();
            textBoxHometown.Text = UpdatedStudent.QueQuan;

            buttonUpdate.Click += (_, e) =>
            {
                UpdatedStudent = GetStudent();
                UpdateStudent?.Invoke(buttonUpdate, e);
            };

            buttonCancel.Click += (_, e) =>
            {
                Close();
            };
        }

        private HocSinh GetStudent()
        {
            return new HocSinh
            {
                HocSinhID = textBoxId.Text.Trim(),
                TenHocSinh = textBoxName.Text.Trim(),
                NamSinh = Int32.Parse(textBoxBirthYear.Text),
                DiemTrungBinh = float.Parse(textBoxGPA.Text),
                QueQuan = textBoxHometown.Text.Trim(),
                LopHocID = comboBoxClass.SelectedValue.ToString()
            };
        }

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
        }

        private void SetupComboBoxClass()
        {
            comboBoxClass.DataSource = BindingSourceClass;
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";
        }

        public HocSinh UpdatedStudent { get; set; }
        public BindingSource BindingSourceClass { get; set; }
        public BindingSource BindingSourceStudent { get; set; }
        public QLHSDataContext Db { get; set; }

        public event EventHandler UpdateStudent;
    }
}
