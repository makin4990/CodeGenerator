using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
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
        public string ToClass(string classNamsace)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"namespace classNamsace;");
            if (classNamsace.Contains("Domain"))
            {
                builder.AppendLine($"{AccessModifier} class {Name}:Entity");
            }
            else
            {
                builder.AppendLine($"{AccessModifier} class {Name}");
            }
            builder.AppendLine("{");

            foreach (var property in ClassProperties)
            {
                builder.AppendLine(property.ToString());
            }

            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
