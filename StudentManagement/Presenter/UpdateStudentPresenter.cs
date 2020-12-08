using StudentManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.Presenter
{
    public class UpdateStudentPresenter
    {
        IUpdateStudentView view;

        public UpdateStudentPresenter(IUpdateStudentView view)
        {
            this.view = view;
            view.UpdateStudent += View_UpdateStudent;
        }

        private void View_UpdateStudent(object sender, EventArgs e)
        {
            var student = view.Db.HocSinhs.SingleOrDefault(s => s.HocSinhID == view.UpdatedStudent.HocSinhID);
            student.TenHocSinh = view.UpdatedStudent.TenHocSinh;
            student.NamSinh = view.UpdatedStudent.NamSinh;
            student.DiemTrungBinh = view.UpdatedStudent.DiemTrungBinh;
            student.QueQuan = view.UpdatedStudent.QueQuan;
            student.LopHocID = view.UpdatedStudent.LopHocID;
            view.Db.SubmitChanges();
        }
    }
}
