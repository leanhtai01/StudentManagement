using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.View
{
    public interface IStudentManagementView
    {
        BindingSource BindingSourceClass { get; set; }
        BindingSource BindingSourceStudent { get; set; }
        event EventHandler UpdateClass;
        event EventHandler DeleteClass;
        event EventHandler CreateStudent;
        event EventHandler UpdateStudent;
        event EventHandler DeleteStudent;
        event EventHandler LoadData;
    }
}
