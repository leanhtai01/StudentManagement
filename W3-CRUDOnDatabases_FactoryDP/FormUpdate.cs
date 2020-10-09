using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W3_CRUDOnDatabases_FactoryDP
{
    public partial class FormUpdate : Form
    {
        StudentBusinessLogic businessLogic;
        Student student;
        public delegate void StudentUpdatedEventHandler(object sender, EventArgs e);
        public event StudentUpdatedEventHandler StudentUpdated;

        public FormUpdate(Student student)
        {
            businessLogic = new StudentBusinessLogic();
            this.student = student;

            InitializeComponent();
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;

            comboBoxClass.DataSource = businessLogic.GetClassList();
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";

            textBoxId.Text = student.Id;
            textBoxName.Text = student.Name;
            textBoxBirthYear.Text = student.BirthYear.ToString();
            textBoxGPA.Text = student.GPA.ToString();
            textBoxHometown.Text = student.Hometown;
        }

        /// <summary>
        /// make sure user fill all information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        } // end method TextBox_TextChanged

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxBirthYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxGPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            businessLogic.UpdateStudent(GetStudent());
            StudentUpdated?.Invoke(this, EventArgs.Empty);
            MessageBox.Show("Cập nhật học sinh thành công!");
        }

        /// <summary>
        /// create a Student using information from UI
        /// </summary>
        /// <returns></returns>
        private Student GetStudent()
        {
            return new Student
            {
                Id = textBoxId.Text.Trim(),
                Name = textBoxName.Text.Trim(),
                BirthYear = Int32.Parse(textBoxBirthYear.Text),
                GPA = Double.Parse(textBoxGPA.Text),
                Hometown = textBoxHometown.Text.Trim(),
                ClassId = comboBoxClass.SelectedValue.ToString()
            };
        }
    }
}
