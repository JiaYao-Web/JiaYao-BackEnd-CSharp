using System;
using System.Collections.Generic;

#nullable disable

namespace JiaYao.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }
}
