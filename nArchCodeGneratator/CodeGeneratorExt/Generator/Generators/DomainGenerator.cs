using System.Collections.Generic;
using System.IO;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator
{
    public class DomainGenerator : Generator
    {
        private readonly List<ClassModel> _classList;
        private readonly string _basePath;

        public DomainGenerator(List<ClassModel> classList, string basePath)
        {
            _classList = classList;
            _basePath = basePath;
        }

        public override void Generate()
        {
            string nameSpace = "Domain";
            string filePath = _basePath  +  @"\Entities";
            foreach (var @class in _classList)
            {
                string fullPath = Path.Combine(filePath, (@class.Name+".cs"));
                var classCode = @class.ToClass(nameSpace);
                File.WriteAllText(fullPath, classCode);

            }
        }
    }

}

