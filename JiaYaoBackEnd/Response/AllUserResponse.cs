using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Response
{
    // 首页所有用户
    public class AllUserResponse
    {
        public int id { set; get; }
        public string name { set; get; }
        public string image { set; get; }
        public bool isMyself { get; set; }
        public bool ifFollow { get; set; }
        public int fanNumber { get; set; }
    }
}
