using System.Collections.Generic;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator.Factories
{
    public class ApiFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList,string basePath)
        {
            return new ApiGenerator(classNameList, basePath);
        }
    }
}
