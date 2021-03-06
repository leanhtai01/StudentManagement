﻿using StudentManagement.Model;
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

        public ClassAddPresenter(IClassAddView view)
        {
            this.view = view;
            view.Add += View_Add;
        }

        private void View_Add(object sender, EventArgs e)
        {
            view.BindingSourceClass.Add((LopHoc)view.AddedClass);
            view.Db.LopHocs.InsertOnSubmit(view.AddedClass);
            view.Db.SubmitChanges();
        }
    }
}
