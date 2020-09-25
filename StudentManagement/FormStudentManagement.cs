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
    public partial class FormStudentManagement : Form
    {
        private DataHandling dataHandling = new DataHandling();
        private List<Student> listStudents;
        private string filename = "dshocsinh.txt";

        public FormStudentManagement()
        {
            InitializeComponent();
        }

        private void FillData()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("Họ tên", typeof(string));
            dataTable.Columns.Add("Ngày sinh", typeof(DateTime));
            dataTable.Columns.Add("Quê quán", typeof(string));
            dataTable.Columns.Add("Điểm trung bình", typeof(double));

            listStudents = dataHandling.ReadData(filename);
            for (int i = 0; i < listStudents.Count; i++)
            {
                dataTable.Rows.Add(i + 1, listStudents[i].FullName, listStudents[i].BirthDate, listStudents[i].Hometown, listStudents[i].GPA);
            }

            dataGridViewStudent.DataSource = dataTable;
            dataGridViewStudent.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            FillData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAdd formAdd = new FormAdd();
            formAdd.Show();
        }
    }
}
