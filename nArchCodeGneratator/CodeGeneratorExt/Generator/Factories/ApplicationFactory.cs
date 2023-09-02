using System.Collections.Generic;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator.Factories
{
    public class ApplicationFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList, string basePath)
        {
            return new ApplicationGenerator(classNameList, basePath);
        }
    }
}
