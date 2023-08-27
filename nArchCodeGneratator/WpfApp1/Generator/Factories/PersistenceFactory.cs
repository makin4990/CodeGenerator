using System.Collections.Generic;

namespace WpfApp1.Generator.Factories
{
    public class PersistenceFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList)
        {
            return new PersistenceGenerator(classNameList);
        }
    }
}
