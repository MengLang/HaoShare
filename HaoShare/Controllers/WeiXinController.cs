using HaoShare.Core;
using HaoShare.Core.Business;
using HaoShare.WeiXin;
using HaoShare.WeiXin.Model;
using HaoShare.WeiXin.ResponseLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaoShare.Controllers
{
    public class WeiXinController : Controller
    {
        private static readonly string Token =HConfig.Token;
        public static readonly string EncodingAESKey = HConfig.EncodingAESKey;
        private static readonly string AppId =HConfig.AppId;
        private static readonly string Key = HConfig.Key;
        /// <summary>
        /// 验证Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content(DateTime.Now.ToString());
            }
        }
        /// <summary>
        /// 接收推送消息，响应消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }
            try
            {
                string postString = "";
                using (Stream stream = Request.InputStream)
                {
                    byte[] postByte = new byte[stream.Length];
                    stream.Read(postByte, 0, (int)stream.Length);
                    postString = System.Text.Encoding.UTF8.GetString(postByte);
                }

                WeiXinMsgModel model = new WeiXinMsgModel();
                string msgType = ResponseLogic.GetModelAndTypeByPostString(postString, out model);
                string msg = "";
                if (model != null)
                {
                    msg = ResponseLogic.ResponseMessage(model);
                    return Content(msg);
                }
            }
            catch (Exception ex)
            {
                LogCore.LogSave("获取微信消息异常", ex.Message, ex.HResult.ToString());
            }
            return Content("success");

        }
    }
}