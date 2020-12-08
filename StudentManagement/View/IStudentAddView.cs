using StudentManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.View
{
    public interface IStudentAddView
    {
        HocSinh AddedStudent { get; set; }
        BindingSource BindingSourceClass { get; set; }
        BindingSource BindingSourceStudent { get; set; }
        QLHSDataContext Db { get; set; }
        event EventHandler Add;
    }
}
