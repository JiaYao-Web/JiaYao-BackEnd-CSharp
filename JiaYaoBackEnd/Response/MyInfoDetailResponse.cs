using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Response
{
    public class MyInfoDetailResponse
    {
        public string name { get; set; }
        public string email { get; set; }
        public int userId { get; set; }
        public string image { get; set; }
        public bool isMyself { get; set; }
        public bool ifFollow { get; set; }
        public int followNumber { get; set; }
    }
}
