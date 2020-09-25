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

        private void ReFillData(List<Student> students)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("MSSV", typeof(string));
            dataTable.Columns.Add("Họ tên", typeof(string));
            dataTable.Columns.Add("Ngày sinh", typeof(DateTime));
            dataTable.Columns.Add("Quê quán", typeof(string));
            dataTable.Columns.Add("Điểm trung bình", typeof(double));

            //LoadDataFromFile();
            for (int i = 0; i < students.Count; i++)
            {
                dataTable.Rows.Add(i + 1, students[i].ID, students[i].FullName, students[i].BirthDate, students[i].Hometown, students[i].GPA);
            }

            dataGridViewStudent.DataSource = dataTable;
            dataGridViewStudent.Columns["STT"].ReadOnly = true;
            dataGridViewStudent.Columns["MSSV"].ReadOnly = true;
            dataGridViewStudent.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewStudent.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadDataFromFile();
            ReFillData(listStudents);
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
            ReFillData(listStudents);
        }

        private void LoadDataFromFile()
        {
            listStudents = dataHandling.ReadData(filename);
        }

        public void OnStudentAdded(object sender, EventArgs e)
        {
            ReFillData(listStudents);
            dataHandling.WriteData("dshocsinh.txt", listStudents);
        }

        public void OnStudentFound(object sender, EventArgs e, List<Student> list)
        {
            ReFillData(list);
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FormSearch formSearch = new FormSearch(listStudents);
            formSearch.StudentFound += (sendder, ed) => OnStudentFound(sender, e, formSearch.Result);
            formSearch.Show();
        }
    }
}
