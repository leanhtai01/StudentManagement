using StudentManagement.Model;
using StudentManagement.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.Presenter
{
    public class StudentManagementPresenter
    {
        IStudentManagementView view;
        //BindingSource bindingSourceClass;
        QLHSDataContext db;

        public StudentManagementPresenter(IStudentManagementView view)
        {
            db = new QLHSDataContext();
            this.view = view;
            view.Db = db;
            view.LoadData += View_LoadData;
            view.UpdateClass += View_UpdateClass;
            view.DeleteClass += View_DeleteClass;
            view.DeleteStudent += View_DeleteStudent;
        }

        private void View_DeleteStudent(object sender, EventArgs e)
        {
            db.HocSinhs.DeleteOnSubmit((HocSinh)view.BindingSourceStudent.Current);
            view.BindingSourceStudent.RemoveCurrent();
            db.SubmitChanges();
        }

        private void View_DeleteClass(object sender, EventArgs e)
        {
            view.BindingSourceClass.RemoveCurrent();
            db.SubmitChanges();
        }

        private void View_UpdateClass(object sender, EventArgs e)
        {
            db.SubmitChanges();
        }

        private void View_LoadData(object sender, EventArgs e)
        {
            BindingSource bindingSourceClass = new BindingSource
            {
                DataSource = db.LopHocs
            };
            view.BindingSourceClass = bindingSourceClass;

            BindingSource bindingSourceStudent = new BindingSource(bindingSourceClass, "HocSinhs");
            view.BindingSourceStudent = bindingSourceStudent;
        }
    }
}
