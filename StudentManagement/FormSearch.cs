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
    public partial class FormSearch : Form
    {
        List<Student> listStudents;
        List<Student> result = null;
        public delegate void StudentFoundEventHandler(object sender, EventArgs e);
        public event StudentFoundEventHandler StudentFound;

        public List<Student> Result { get => result; set => result = value; }

        public FormSearch(List<Student> listStudents)
        {
            InitializeComponent();
            this.listStudents = listStudents;
        }

        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            //buttonFilter.Enabled = false;

            //foreach (ComboBox comboBox in tableLayoutPanel1.Controls.OfType<ComboBox>())
            //{
            //    if (!string.IsNullOrEmpty(comboBox.Text.Trim()))
            //    {
            //        buttonFilter.Enabled = true;
            //        break;
            //    }
            //}
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            //buttonFilter.Enabled = false;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            Result = null;

            if (!string.IsNullOrEmpty(comboBoxID.Text.Trim()))
            {
                result = new List<Student>(listStudents.FindAll(s => s.ID == comboBoxID.Text.Trim()));
            }
            else
            {
                // FullName
                if (!string.IsNullOrEmpty(comboBoxFullName.Text.Trim()))
                {
                    if (result == null)
                    {
                        result = new List<Student>(listStudents.FindAll(s => s.FullName == comboBoxFullName.Text.Trim()));
                    }
                    else
                    {
                        result = result.FindAll(s => s.FullName == comboBoxFullName.Text.Trim());
                    }
                }

                // Age
                if (result == null)
                {
                    result = new List<Student>(listStudents.FindAll(s => DateTime.Now.Year - s.BirthDate.Year == numericUpDown1.Value));
                }
                else
                {
                    result = result.FindAll(s => DateTime.Now.Year - s.BirthDate.Year == numericUpDown1.Value);
                }

                // Hometown
                if (!string.IsNullOrEmpty(comboBoxHometown.Text.Trim()))
                {
                    if (result == null)
                    {
                        result = new List<Student>(listStudents.FindAll(s => s.Hometown == comboBoxHometown.Text.Trim()));
                    }
                    else
                    {
                        result = result.FindAll(s => s.Hometown == comboBoxHometown.Text.Trim());
                    }
                }

                // GPA
                if (!string.IsNullOrEmpty(comboBoxGPA.Text.Trim()))
                {
                    if (result == null)
                    {
                        result = new List<Student>(listStudents.FindAll(s => s.GPA == double.Parse(comboBoxGPA.Text.Trim())));
                    }
                    else
                    {
                        result = result.FindAll(s => s.GPA == double.Parse(comboBoxGPA.Text.Trim()));
                    }
                }
            }

            if (result == null || result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy bất kỳ học sinh nào!");
            }
            else
            {
                StudentFound?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
