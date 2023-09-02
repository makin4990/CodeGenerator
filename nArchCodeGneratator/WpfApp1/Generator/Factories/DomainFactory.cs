using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Generator.Factories
{
    public class DomainFactory : ICodeGeneratorFactory
    {
        public Generator Generate(List<ClassModel> classNameList)
        {
            return new DomainGenerator(classNameList);
        }
    }
}
