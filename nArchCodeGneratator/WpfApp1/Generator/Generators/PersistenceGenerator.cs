using System.Collections.Generic;

namespace WpfApp1.Generator
{
    public class PersistenceGenerator : Generator
    {
        private readonly List<ClassModel> _classList;

        public PersistenceGenerator(List<ClassModel> classList)
        {
            _classList = classList;
        }

        public override void Generate()
        {
            // Generate and define the behavior for ProjectB
        }
    }

}