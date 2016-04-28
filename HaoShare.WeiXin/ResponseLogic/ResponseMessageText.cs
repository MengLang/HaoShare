using HaoShare.Core;
using HaoShare.WeiXin.Core;
using HaoShare.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaoShare.WeiXin.ResponseLogic
{
    public class ResponseMessageText
    {
        private static string XmlTemplete = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[{3}]]></MsgType><Content><![CDATA[{4}]]></Content></xml>";
       
        public static string Message(WeiXinMsgModel model)
        {
            var keyword = model.Content;
            string NewContent = "哈喽，奥哈伊，我们还在测试中";
            if (keyword.Contains("你好"))
            {
                var desKey = RC4Crypto.Encrypt(HConfig.Key, model.FromUserName);
                NewContent = string.Format("你好，欢迎来到HaoShare\n<a href=\"http://www.oazjg.com/Home/Index/?ST={0}\">我要注册</a>\n<a href=\"http://www.oazjg.com/Home/Index/?ST={1}\">我要登录</a>",desKey,desKey);
            }

            return string.Format(XmlTemplete, model.FromUserName, model.ToUserName, DateTime.Now.Ticks, model.MsgType, NewContent);
        }
    }
}