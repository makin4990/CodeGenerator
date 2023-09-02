﻿using System.Collections.Generic;
using System.IO;
using WpfApp1.Models;

namespace WpfApp1.Generator
{
    public class DomainGenerator : Generator
    {
        private readonly List<ClassModel> _classList;

        public DomainGenerator(List<ClassModel> classList)
        {
            _classList = classList;
        }

        public override void Generate()
        {
            string nameSpace = "Domain";
            string filePath = string.Empty;
            foreach (var @class in _classList)
            {
                string fullPath = Path.Combine(filePath, (@class.Name+".cs"));
                var classCode = @class.ToClass(nameSpace);
                File.WriteAllText(fullPath, classCode);

            }
        }
    }

}

