using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Request
{
    public class ChangePasswordRequest
    {
        public string email { get; set; }

        public string oldPassword { get; set; }

        public string newPassword { get; set; }
    }
}
