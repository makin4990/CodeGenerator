using System.Collections.Generic;

namespace WpfApp1.Generator.Factories
{
    public class ApplicationFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList)
        {
            return new ApplicationGenerator(classNameList);
        }
    }
}
