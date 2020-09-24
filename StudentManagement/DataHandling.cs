using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    class DataHandling
    {
        public List<Student> ReadData(string filename)
        {
            var listStudents = new List<Student>();

            if (File.Exists(filename))
            {
                using (var sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        var info = sr.ReadLine().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        var date = info[2].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        var birthDate = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
                        var student = new Student
                        {
                            ID = info[0],
                            FullName = info[1],
                            BirthDate = birthDate,
                            Hometown = info[3],
                            GPA = Double.Parse(info[4])
                        };

                        listStudents.Add(student);
                    }
                }
            }

            return listStudents;
        }

        public void WriteData(string filename, List<Student> listStudents)
        {
            using (var sw = new StreamWriter(filename))
            {
                for (int i = 0; i < listStudents.Count; i++)
                {
                    string line = listStudents[i].ID + '#' + listStudents[i].FullName + '#' + listStudents[i].BirthDate.ToString("dd/MM/yyyy") + '#' + listStudents[i].Hometown + '#' + listStudents[i].GPA;
                    sw.WriteLine(line);
                }
            }
        }
    }
}
