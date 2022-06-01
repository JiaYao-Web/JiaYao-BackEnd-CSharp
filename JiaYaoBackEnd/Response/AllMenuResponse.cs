using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Response
{
    public class AllMenuResponse
    {
        public int menuId { set; get; }
        public string menuName { set; get; }
        public string menuImage { set; get; }
        public string category { set; get; }
        public int userId { set; get; }
        public string userName { set; get; }
        public string userImage { set; get; }
    }
}
