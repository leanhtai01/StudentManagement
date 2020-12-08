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
    public partial class FormAddClass : Form, IClassAddView
    {
        public FormAddClass(BindingSource bindingSourceClass, QLHSDataContext db)
        {
            InitializeComponent();

            ClassAddPresenter presenter = new ClassAddPresenter(this);

            buttonAdd.Enabled = false;
            textBoxId.TextChanged += TextBox_TextChanged;
            textBoxName.TextChanged += TextBox_TextChanged;
            buttonAdd.Click += (_, e) =>
            {
                Db = db;
                AddedClass = GetClass();
                BindingSourceClass = bindingSourceClass;
                Add?.Invoke(buttonAdd, e);
                MessageBox.Show("Thêm lớp học thành công!");
            };
            buttonCancel.Click += ButtonCancel_Click;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
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

        private LopHoc GetClass()
        {
            return new LopHoc
            {
                LopHocID = textBoxId.Text.Trim(),
                TenLopHoc = textBoxName.Text.Trim()
            };
        }

        public LopHoc AddedClass { get; set; }
        public BindingSource BindingSourceClass { get; set; }
        public QLHSDataContext Db { get; set; }

        public event EventHandler Add;
    }
}
