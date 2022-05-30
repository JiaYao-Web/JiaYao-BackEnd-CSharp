using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Response
{
    public class MyInfoResponse
    {
        public string msg { get; set; }
        public bool status { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int userId { get; set; }
        public string image { get; set; }
    }
}
