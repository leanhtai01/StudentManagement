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
    public partial class FormAddClass : Form
    {
        ClassBusinessLogic classBL;
        public delegate void ClassAddedEventHandler(object sender, EventArgs e);
        public event ClassAddedEventHandler ClassAdded;

        public FormAddClass()
        {
            classBL = new ClassBusinessLogic();

            InitializeComponent();
        }

        private void FormAddClass_Load(object sender, EventArgs e)
        {
            buttonAdd.Enabled = false;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// create a Class using information from UI
        /// </summary>
        /// <returns></returns>
        private Class GetClass()
        {
            return new Class
            {
                Id = textBoxId.Text.Trim(),
                Name = textBoxName.Text.Trim()
            };
        } // end class GetClass

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (classBL.IsClassExists(textBoxId.Text.Trim()))
            {
                MessageBox.Show("Lớp đã tồn tại! Không thể thêm!");
            }
            else
            {
                classBL.InsertClass(GetClass());
                ClassAdded?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Thêm lớp học thành công!");
            }
        }
    }
}
