using StudentManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.View
{
    public interface IClassAddView
    {
        LopHoc AddedClass { get; set; }
        BindingSource BindingSourceClass { get; set; }
        event EventHandler Add;
    }
}
