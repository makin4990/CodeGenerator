using System.Collections.Generic;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator.Factories
{
    public class InfrastructureFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList, string basePath)
        {
            return new InfrastructureGenerator(classNameList, basePath);
        }
    }
}
