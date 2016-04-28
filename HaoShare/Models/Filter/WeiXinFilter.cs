using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Swagger.Models
{
    public class WeiXinFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var userAgent = context.Request.UserAgent.ToLower();
            if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("micromessenger"))
            {
                filterContext.Result = new RedirectResult("/WeiXin/QueryAccessToken");
            }

            //base.OnActionExecuting(filterContext);
        }
    }
}