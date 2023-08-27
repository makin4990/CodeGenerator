using System.Collections.Generic;

namespace WpfApp1.Generator.Factories
{
    public class ApiFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList)
        {
            return new ApiGenerator(classNameList);
        }
    }
}
