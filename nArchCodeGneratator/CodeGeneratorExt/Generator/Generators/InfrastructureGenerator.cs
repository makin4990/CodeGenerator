using System.Collections.Generic;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator
{
    public class InfrastructureGenerator : Generator
    {
        private readonly List<ClassModel> _classList;
        private readonly string _basePath;


        public InfrastructureGenerator(List<ClassModel> classList, string basePath)
        {
            _classList = classList;
            _basePath = basePath;
        }

        public override void Generate()
        {
            // Generate and define the behavior for ProjectB
        }
    }

}