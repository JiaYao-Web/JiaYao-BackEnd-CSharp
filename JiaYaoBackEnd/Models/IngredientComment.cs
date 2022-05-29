using System;
using System.Collections.Generic;

#nullable disable

namespace JiaYao.Models
{
    public partial class IngredientComment
    {
        public int IngredientId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public TimeSpan? Time { get; set; }
    }
}
