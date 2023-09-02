using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorExt.Models
{
    public class ClassModel
    {
        public ClassModel()
        {
            ClassProperties = new ObservableCollection<ClassProperty>();
        }
        public string Id { get; set; }
        public string AccessModifier { get; set; }
        public string Name { get; set; }
        public virtual ObservableCollection<ClassProperty> ClassProperties { get; set; }
        public string ToClass(string classNamespace)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"using CoreFramework.Persistence.Repositories;");
            builder.AppendLine($"");
            builder.AppendLine($"namespace {classNamespace};");
            if (classNamespace.Contains("Domain"))
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

        public string ToCreateDto()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"namespace BloodBrother.Application.Features.{Name}Features.Dtos;");
            builder.AppendLine("");
            builder.AppendLine($"public class Create{Name}Dto");
            builder.AppendLine("{");
            foreach (var property in ClassProperties)
            {
                builder.AppendLine(property.ToString());
            }
            builder.AppendLine("}");

            return builder.ToString();
        }
        public string ToUpdateDto()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"namespace BloodBrother.Application.Features.{Name}Features.Dtos;");
            builder.AppendLine("");
            builder.AppendLine($"public class Update{Name}Dto");
            builder.AppendLine("{");
            foreach (var property in ClassProperties)
            {
                builder.AppendLine(property.ToString());
            }
            builder.AppendLine("}");

            return builder.ToString();
        }
        public string ToListModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"namespace BloodBrother.Application.Features.{Name}Features.Dtos;");
            builder.AppendLine("");
            builder.AppendLine($"public class {Name}ListModelDto");
            builder.AppendLine("{");
            foreach (var property in ClassProperties)
            {
                builder.AppendLine(property.ToString());
            }
            builder.AppendLine("}");

            return builder.ToString();
        }
        public string ToDto()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"namespace BloodBrother.Application.Features.{Name}Features.Dtos;");
            builder.AppendLine("");
            builder.AppendLine($"public class {Name}Dto");
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
