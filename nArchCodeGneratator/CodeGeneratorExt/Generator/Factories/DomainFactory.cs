using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator.Factories
{
    public class DomainFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList, string basePath)
        {
            return new DomainGenerator(classNameList,basePath);
        }
    }
}
