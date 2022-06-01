using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Response
{
    public class MenuDetailResponse
    {
        public int id { set; get; }
        public string name { set; get; }
        public string category { set; get; }
        public string content { set; get; }
        public string introduction { set; get; }
        public string image { set; get; }
        public int favoriteNumber { set; get; }
        public int likeNumber { set; get; }
        public bool ifFavorite { set; get; }
        public bool ifLike { set; get; }
    }
}
