using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private void ReFillData()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("MSSV", typeof(string));
            dataTable.Columns.Add("Họ tên", typeof(string));
            dataTable.Columns.Add("Ngày sinh", typeof(DateTime));
            dataTable.Columns.Add("Quê quán", typeof(string));
            dataTable.Columns.Add("Điểm trung bình", typeof(double));

            //LoadDataFromFile();
            for (int i = 0; i < listStudents.Count; i++)
            {
                dataTable.Rows.Add(i + 1, listStudents[i].ID, listStudents[i].FullName, listStudents[i].BirthDate, listStudents[i].Hometown, listStudents[i].GPA);
            }

            dataGridViewStudent.DataSource = dataTable;
            dataGridViewStudent.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //LoadDataFromFile();
            ReFillData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAdd formAdd = new FormAdd(listStudents);
            formAdd.StudentAdded += OnStudentAdded;
            formAdd.Show();
        }

        private void FormStudentManagement_Load(object sender, EventArgs e)
        {
            LoadDataFromFile();
            ReFillData();
        }

        private void LoadDataFromFile()
        {
            listStudents = dataHandling.ReadData(filename);
        }

        public void OnStudentAdded(object sender, EventArgs e)
        {
            ReFillData();
        }
    }
}
