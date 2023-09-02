using System;

namespace CodeGeneratorExt.Models
{
    public class ClassProperty
    {
        public string Id { get; set; }
        public string ClassId { get; set; }
        public string AccessModifier { get; set; } 
        public string Type { get; set; } 
        public string Name { get; set; }
        public string FullText => $"    {AccessModifier} {Type} {Name} {{get; set;}}";

        public override string ToString()
        {
            return $"    {AccessModifier} {Type} {Name} {{get; set;}}";
        }

    }
}