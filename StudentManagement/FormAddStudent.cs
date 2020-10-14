using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class FormAddStudent : Form
    {
        StudentBusinessLogic businessLogic;
        public delegate void StudentAddedEventHandler(object sender, EventArgs e);
        public event StudentAddedEventHandler StudentAdded;

        public FormAddStudent()
        {
            businessLogic = new StudentBusinessLogic();

            InitializeComponent();
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            buttonAdd.Enabled = false;

            comboBoxClass.DataSource = businessLogic.GetClassList();
            comboBoxClass.DisplayMember = "TenLopHoc";
            comboBoxClass.ValueMember = "LopHocID";
        }

        /// <summary>
        /// make sure user fill all information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        } // end method TextBox_TextChanged

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
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

        /// <summary>
        /// // only allow non-negative integers in textBoxBirthYear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxBirthYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// only allow non-negative real number in textBoxGPA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxGPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        } // end method TextBoxGPA_KeyPress

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (businessLogic.IsStudentExists(textBoxId.Text.Trim()))
            {
                MessageBox.Show("Học sinh đã tồn tại! Không thể thêm!");
            }
            else
            {
                businessLogic.InsertStudent(GetStudent());
                StudentAdded?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Thêm học sinh thành công!");
            }
        } // end method ButtonAdd_Click
    }
}
