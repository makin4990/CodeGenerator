using System.Collections.Generic;

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
