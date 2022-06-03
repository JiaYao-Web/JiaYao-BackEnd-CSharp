using System;
using System.Collections.Generic;

#nullable disable

namespace JiaYao.Models
{
    public partial class Follow
    {
        public int FollowId { get; set; }
        public int FollowedId { get; set; }
    }
}
