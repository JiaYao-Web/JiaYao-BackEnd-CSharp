using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Request
{
    public class UploadIngredientRequest
    {
        public string name { get; set; }
        public string category { get; set; }
        public string introduction { get; set; }
        public IFormFile file { get; set; }
    }
}
