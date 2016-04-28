using HaoShare.Core;
using HaoShare.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaoShare.Models.Filter
{
    /// <summary>
    /// 检测用户跳转过来的参数
    /// </summary>
    public class CheckUserStatusFilter:ActionFilterAttribute
    {
        /// <summary>
        /// 重写校验用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var controller = filterContext.Controller;
            var st = context.Request.QueryString["ST"];
            if (!string.IsNullOrEmpty(st))
            {
                var des = HaoShare.WeiXin.Core.RC4Crypto.Decrypt(HConfig.Key, st);
                var userInfo = HaoShare.Core.Business.UserCore.QueryUserByOID(des);
                if (userInfo != null)
                {
                    HttpContext.Current.Session["UserInfo"] = userInfo;
                }
                LogCore.LogSave("记录ST", st + "|" + des, "000");
            }
            
        }
    }
}