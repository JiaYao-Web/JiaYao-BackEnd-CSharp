using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Authorization
{
    public class ApiAuthorize : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           if (context.Filters.Contains(new MyNoAuthentication()))
           {
                return;
           }
            var authorize = context.HttpContext.Request.Headers["MyAuthentication"];
            if (string.IsNullOrEmpty(authorize))
            {
                context.Result = new JsonResult("请求参数不能为空");
                return;
            }
            if (!MemoryCacheHelper.Exists(authorize))
            {
                context.Result = new JsonResult("无效的授权信息或者授权信息已过期");
                return;
            }

        }
    }
}
