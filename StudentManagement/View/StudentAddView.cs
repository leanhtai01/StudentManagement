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
    public partial class FormAddStudent : Form, IStudentAddView
    {
        public FormAddStudent(BindingSource bindingSourceClass, BindingSource bindingSourceStudent, QLHSDataContext db)
        {
            InitializeComponent();

            StudentAddPresenter presenter = new StudentAddPresenter(this);

            buttonAdd.Enabled = false;
            BindingSourceClass = bindingSourceClass;
            BindingSourceStudent = bindingSourceStudent;

            buttonAdd.Click += (_, e) =>
            {
                Db = db;
                AddedStudent = GetStudent();
                Add?.Invoke(buttonAdd, e);
            };

            buttonCancel.Click += (_, e) =>
            {
                Close();
            };

            textBoxId.TextChanged += TextBox_TextChanged;
            textBoxName.TextChanged += TextBox_TextChanged;
            textBoxBirthYear.TextChanged += TextBox_TextChanged;
            textBoxGPA.TextChanged += TextBox_TextChanged;
            textBoxHometown.TextChanged += TextBox_TextChanged;
            InitializeComboBoxClass();
        }

        private void InitializeComboBoxClass()
        {
            comboBoxClass.DataSource = BindingSourceClass;
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";
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
            buttonAdd.Enabled = true;

            foreach (TextBox textBox in tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(textBox.Text.Trim()))
                {
                    buttonAdd.Enabled = false;
                    break;
                }
            }
        }

        public BindingSource BindingSourceClass { get; set; }
        public BindingSource BindingSourceStudent { get; set; }
        public HocSinh AddedStudent { get; set; }
        public QLHSDataContext Db { get; set; }

        public event EventHandler Add;
    }
}
