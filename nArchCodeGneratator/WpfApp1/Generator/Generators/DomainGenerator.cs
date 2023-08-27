using System.Collections.Generic;

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
        }
    }

}

