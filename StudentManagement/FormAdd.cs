using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class FormAdd : Form
    {
        List<Student> listStudents;
        public delegate void StudentAddedEventHandler(object sender, EventArgs e);
        public event StudentAddedEventHandler StudentAdded;

        public FormAdd(List<Student> listStudents)
        {
            InitializeComponent();
            this.listStudents = listStudents;
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            dateTimePickerBirthDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerBirthDate.CustomFormat = "dd/MM/yyyy";
            buttonAdd.Enabled = false;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var student = GetStudent();
            
            if (listStudents.Find(s => s.ID == student.ID) != null)
            {
                MessageBox.Show("Học sinh đã tồn tại! Không thể thêm!");
            }
            else
            {
                listStudents.Add(student);
                StudentAdded?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Thêm học sinh thành công!");
            }
        }

        private Student GetStudent()
        {
            return new Student
            {
                ID = textBoxID.Text.Trim(),
                FullName = textBoxFullName.Text.Trim(),
                BirthDate = dateTimePickerBirthDate.Value,
                Hometown = textBoxHometown.Text.Trim(),
                GPA = Double.Parse(textBoxGPA.Text)
            };
        }
    }
}
