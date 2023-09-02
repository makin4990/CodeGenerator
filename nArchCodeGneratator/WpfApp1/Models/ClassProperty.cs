using System;

namespace WpfApp1.Models
{
    public class ClassProperty
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public string AccessModifier { get; set; } 
        public string Type { get; set; } 
        public string Name { get; set; }
        public string FullText => $"{AccessModifier} {Type} {Name} {{get; set;}}";

        public override string ToString()
        {
            return $"{AccessModifier} {Type} {Name} {{get; set;}}";
        }

    }
}