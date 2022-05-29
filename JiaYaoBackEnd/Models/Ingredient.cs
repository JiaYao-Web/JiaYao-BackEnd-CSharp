using System;
using System.Collections.Generic;

#nullable disable

namespace JiaYao.Models
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Introduction { get; set; }
        public string Category { get; set; }
    }
}
