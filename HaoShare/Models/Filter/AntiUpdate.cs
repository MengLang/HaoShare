using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AntiUpdate:ActionFilterAttribute
    {
        string cookName = "_fo";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            string newCookieToken;
            string formToken;
            if (context.Request.Cookies.AllKeys.Contains(cookName))
            {
                AntiForgery.GetTokens(context.Request.Cookies[cookName].Value, out newCookieToken, out formToken);
            }
            else
            {
                AntiForgery.GetTokens(null, out newCookieToken, out formToken);
            }
            context.Items["FormToken"] = formToken;
            context.Items["CookieToken"] = newCookieToken;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var context = filterContext.HttpContext;
            string newCookieToken = context.Items["CookieToken"] as string;
            if (newCookieToken != null)
            {
                if (context.Request.Cookies.AllKeys.Contains(cookName))
                {
                    context.Response.Cookies.Set(new HttpCookie(cookName, newCookieToken)
                    {
                        Value = newCookieToken,
                        HttpOnly = true,
                        Path = "/",
                        Secure =false,
                    });
                }
                else
                {
                    context.Response.Cookies.Add(new HttpCookie(cookName, newCookieToken)
                    {
                        Value = newCookieToken,
                        HttpOnly = true,
                        Path = "/",
                        Secure = false,
                    });
                }
            }
        }
    }
}