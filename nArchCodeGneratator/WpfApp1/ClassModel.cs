using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ClassModel
    {
        public ClassModel()
        {
            ClassProperties = new ObservableCollection<ClassProperty>();
        }
        public Guid Id { get; set; }
        public string AccessModifier { get; set; }
        public string Name { get; set; }
        public virtual ObservableCollection<ClassProperty> ClassProperties { get; set; }
    }
}
