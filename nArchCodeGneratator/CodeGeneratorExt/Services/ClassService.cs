using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CodeGeneratorExt.Generator;
using CodeGeneratorExt.Generator.Factories;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Services
{
    public sealed class ClassService
    {
        private static readonly ClassService instance = new ClassService();
        public static ClassService Instance => instance;

        public readonly List<ClassModel> ClassModelList;

        private ClassService()
        {
            ClassModelList = new List<ClassModel>();
        }

        public ClassModel AddClass(string accessModifier, string name)
        {
            var @class = new ClassModel() { Id = Guid.NewGuid().ToString(), AccessModifier = accessModifier, Name = name };
            ClassModelList.Add(@class);
            return @class;
        }

        public void RemoveClass(string id)
        {
            var classToDelete = ClassModelList.FirstOrDefault(x => x.Id == id);
            if (classToDelete != null)
            {
                ClassModelList.Remove(classToDelete);
            }
        }

        public ObservableCollection<ClassProperty> AddClassProperty(string classId, string accessModifier, string type, string name)
        {
            var @class = ClassModelList.FirstOrDefault(i => i.Id == classId);
            if (@class != null)
            {
                var property = new ClassProperty() { Id = Guid.NewGuid().ToString(), ClassId = classId, AccessModifier = accessModifier, Type = type, Name = name };
                @class.ClassProperties.Add(property);
            }
            return @class.ClassProperties;
        }

        public ObservableCollection<ClassProperty> GetClassProperties(string classId)
        {
            var properties = ClassModelList.FirstOrDefault(i => i.Id == classId)?.ClassProperties;
            return properties;


        }
        public static void CreateAndSaveProject(List<string> projects)
        {

            List<ClassModel> classNameList = new List<ClassModel>();

            foreach (var project in projects) 
            {
                ICodeGeneratorFactory factory = CreateFactory(project);
                factory.Generate(classNameList,"");
            }

      

        }
        private static ICodeGeneratorFactory CreateFactory(string type)
        {
            switch (type)
            {
                case "Domain":
                   return new DomainFactory();
                case "Application":
                    return new ApplicationFactory();
                case "Infrastructure":
                    return new InfrastructureFactory();
                case "Persistence":
                    return new PersistenceFactory();
                case "Api":
                    return new ApiFactory();
                default:
                    return null;
            }

        }

    }
}
