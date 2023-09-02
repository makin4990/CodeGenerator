using System.Collections.Generic;
using WpfApp1.Models;

namespace WpfApp1.Generator.Factories
{
    public class InfrastructureFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList)
        {
            return new InfrastructureGenerator(classNameList);
        }
    }
}
