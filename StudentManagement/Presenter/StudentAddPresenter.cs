using StudentManagement.Model;
using StudentManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Presenter
{
    public class StudentAddPresenter
    {
        IStudentAddView view;

        public StudentAddPresenter(IStudentAddView view)
        {
            this.view = view;
            view.Add += View_Add;
        }

        private void View_Add(object sender, EventArgs e)
        {
            view.BindingSourceStudent.Add((HocSinh)view.AddedStudent);
            view.Db.HocSinhs.InsertOnSubmit(view.AddedStudent);
            view.Db.SubmitChanges();
        }
    }
}
