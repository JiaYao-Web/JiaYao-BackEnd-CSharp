using System;
using System.Collections.Generic;

#nullable disable

namespace JiaYao.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Introduction { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }
    }
}
