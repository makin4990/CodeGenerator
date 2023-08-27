﻿using System;

namespace WpfApp1
{
    public class ClassProperty
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public string AccessModifier { get; set; } 
        public string Type { get; set; } 
        public string Name { get; set; }
        public string FullText => $"{AccessModifier} {Type} {Name} {{get; set;}}";

    }
}