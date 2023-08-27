using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public sealed class ClassService
    {
        private static readonly ClassService instance = new ClassService();
        public static ClassService Instance => instance;

        public readonly ObservableCollection<ClassModel> ClassModelList;

        private ClassService()
        {
            ClassModelList = new ObservableCollection<ClassModel>();
        }

        public ObservableCollection<ClassModel> AddClass(string accessModifier, string name)
        {
            ClassModelList.Add(new ClassModel() { Id = Guid.NewGuid(), AccessModifier = accessModifier, Name = name });
            return ClassModelList;
        }

        public void RemoveClass(Guid id)
        {
            var classToDelete = ClassModelList.FirstOrDefault(x => x.Id == id);
            if (classToDelete != null)
            {
                ClassModelList.Remove(classToDelete);
            }
        }

        public ObservableCollection<ClassProperty> AddClassProperty(Guid classId, string accessModifier, string type, string name)
        {
            var @class = ClassModelList.FirstOrDefault(i => i.Id == classId);
            if (@class != null)
            {
                var property = new ClassProperty() { Id = Guid.NewGuid(), ClassId = classId, AccessModifier = accessModifier, Type = type, Name = name };
                @class.ClassProperties.Add(property);
            }
            return @class.ClassProperties;
        }

        public ObservableCollection<ClassProperty> GetClassProperties(Guid classId)
        {
            var properties = ClassModelList.FirstOrDefault(i => i.Id == classId).ClassProperties;
            return properties;


        }
    }
}
