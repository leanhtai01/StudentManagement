using StudentManagement.Model;
using StudentManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Presenter
{
    public class ClassAddPresenter
    {
        IClassAddView view;
        QLHSDataContext db;

        public ClassAddPresenter(IClassAddView view)
        {
            this.view = view;
            db = new QLHSDataContext();

            view.Add += View_Add;
        }

        private void View_Add(object sender, EventArgs e)
        {
            view.BindingSourceClass.Add((LopHoc)view.AddedClass);
            db.LopHocs.InsertOnSubmit(view.AddedClass);
            db.SubmitChanges();
        }
    }
}
