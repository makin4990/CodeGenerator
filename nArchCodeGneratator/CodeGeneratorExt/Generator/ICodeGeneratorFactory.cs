using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator
{
    public interface ICodeGeneratorFactory
    {
        Generator Generate(List<ClassModel> classNameList, string basePath);
    }

}


