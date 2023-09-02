using System.Collections.Generic;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator.Factories
{
    public class PersistenceFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList, string basePath)
        {
            return new PersistenceGenerator(classNameList,basePath);
        }
    }
}
