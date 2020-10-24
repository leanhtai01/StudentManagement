using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class FormAddClass : Form
    {
        ClassBusinessLogic classBL;
        public delegate void ClassAddedEventHandler(object sender, EventArgs e);
        public event ClassAddedEventHandler ClassAdded;
        string tableNameClass = "LopHoc";
        DataSet dataSet;
        DbDataAdapter dataAdapter;

        public FormAddClass(string providerName, string connectionStringName)
        {
            classBL = new ClassBusinessLogic(providerName, connectionStringName);

            InitializeComponent();
        }

        public FormAddClass(DataSet dataSet, DbDataAdapter dataAdapter)
        {
            InitializeComponent();

            this.dataSet = dataSet;
            this.dataAdapter = dataAdapter;
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
            Class newClass = GetClass();
            DataTable dataTableClass = dataSet.Tables[tableNameClass];
            DataRow tmpRow = dataTableClass.Rows.Find(newClass.Id);

            if (tmpRow == null)
            {
                DataRow newRow = dataTableClass.NewRow();

                newRow["LopHocID"] = newClass.Id;
                newRow["TenLopHoc"] = newClass.Name;
                dataTableClass.Rows.Add(newRow);

                dataAdapter.Update(dataSet, tableNameClass);

                MessageBox.Show("Thêm lớp học thành công!");
            }
            else
            {
                MessageBox.Show("Lớp đã tồn tại! Không thể thêm!");
            }
        }
    }
}
