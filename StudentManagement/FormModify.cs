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
    public partial class FormModify : Form
    {
        List<Student> listStudents;
        Student student;
        public delegate void StudentModifiedHandler(object sender, EventArgs e);
        public event StudentModifiedHandler StudentModified;

        public FormModify(List<Student> listStudents, Student student)
        {
            InitializeComponent();
            this.listStudents = listStudents;
            this.student = student;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormModify_Load(object sender, EventArgs e)
        {
            dateTimePickerBirthDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerBirthDate.CustomFormat = "dd/MM/yyyy";
            FillData();
            buttonSave.Enabled = false;
        }

        private void FillData()
        {
            textBoxID.Text = student.ID;
            textBoxFullName.Text = student.FullName;
            dateTimePickerBirthDate.Value = student.BirthDate;
            textBoxHometown.Text = student.Hometown;
            textBoxGPA.Text = student.GPA.ToString();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;

            foreach (TextBox textBox in tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(textBox.Text.Trim()))
                {
                    buttonSave.Enabled = false;
                    break;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var index = listStudents.FindIndex(s => s.ID == student.ID);

            listStudents[index].FullName = textBoxFullName.Text;
            listStudents[index].BirthDate = dateTimePickerBirthDate.Value;
            listStudents[index].Hometown = textBoxHometown.Text;
            listStudents[index].GPA = double.Parse(textBoxGPA.Text);

            StudentModified?.Invoke(this, EventArgs.Empty);
        }

        private void dateTimePickerBirthDate_ValueChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }
    }
}
