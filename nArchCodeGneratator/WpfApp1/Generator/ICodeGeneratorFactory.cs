using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Generator
{
    public interface ICodeGeneratorFactory
    {
        Generator Generate(List<ClassModel> classNameList);
    }

}


