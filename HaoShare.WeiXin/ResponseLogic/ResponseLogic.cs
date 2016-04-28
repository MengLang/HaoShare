
using HaoShare.Core;
using HaoShare.Core.Business;
using HaoShare.WeiXin;
using HaoShare.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace HaoShare.WeiXin.ResponseLogic
{
    public class ResponseLogic
    {
        /// <summary>
        /// 获取信息类型
        /// </summary>
        /// <param name="postString"></param>
        /// <returns></returns>
        public static string GetMsgType(string postString) 
        {
            XmlDocument xml = new XmlDocument();
            string msgType = "";
            WeiXinMsgModel model = new WeiXinMsgModel();
            try
            {
                xml.LoadXml(postString);
                XmlNode xmlnode;
                xmlnode = xml.FirstChild;
                msgType = xmlnode["MsgType"].InnerText;

                //msg = model;
            }
            catch (Exception)
            {
               // msg = null;
            }
            return msgType;
        }
        /// <summary>
        /// 根据返回消息类型，发送消息，获取对应的消息实体
        /// </summary>
        /// <param name="postString"></param>
        /// <param name="msgType"></param>
        /// <returns></returns>
        public static WeiXinMsgModel GetModelByType(string postString,string msgType)
        {
            WeiXinMsgModel model = new WeiXinMsgModel();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(postString);
            XmlNode xmlnode;
            xmlnode = xml.FirstChild;

            

            switch (msgType)
            {
                case "text":
                    model.MsgTypeName = "文本";
                    model.Content = xmlnode["Content"].InnerText;
                    break;
                case "image":
                    model.MsgTypeName = "图片";
                    model.PicUrl = xmlnode["PicUrl"].InnerText;
                    model.MediaId = xmlnode["MediaId"].InnerText;
                    break;
                case "voice":
                    model.MsgTypeName = "语音";
                    model.MediaId = xmlnode["MediaId"].InnerText;
                    model.Format = xmlnode["Format"].InnerText;
                    break;
                case "video":
                    model.MsgTypeName = "视频";
                    model.MediaId = xmlnode["MediaId"].InnerText;
                    model.ThumbMediaId = xmlnode["ThumbMediaId"].InnerText;
                    break;
                case "shortvideo":
                    model.MsgTypeName = "短视频";
                    model.MediaId = xmlnode["MediaId"].InnerText;
                    model.ThumbMediaId = xmlnode["ThumbMediaId"].InnerText;
                    break;
                case "location":
                    model.MsgTypeName = "地里位置";
                    model.Location_X = xmlnode["Location_X"].InnerText;
                    model.Location_Y = xmlnode["Location_Y"].InnerText;
                    model.Scale = xmlnode["Scale"].InnerText;
                    model.Label=xmlnode["Label"].InnerText;
                    break;
                case "link":
                    model.MsgTypeName = "链接";
                    model.Title = xmlnode["Title"].InnerText;
                    model.Description = xmlnode["Description"].InnerText;
                    model.Url = xmlnode["Url"].InnerText;
                    break;
                default:
                    break;
            }
            return model;
        }

        /// <summary>
        /// 获取发送消息的类型及实体
        /// </summary>
        /// <param name="postString"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string GetModelAndTypeByPostString(string postString, out WeiXinMsgModel msg)
        {
            WeiXinMsgModel model = new WeiXinMsgModel();
            XmlDocument xml = new XmlDocument();
            string msgType = "";
            try
            {
                xml.LoadXml(postString);
                XmlNode xmlnode;
                xmlnode = xml.FirstChild;
                msgType = xmlnode["MsgType"].InnerText;

                model.ToUserName = xmlnode["ToUserName"].InnerText;
                model.FromUserName = xmlnode["FromUserName"].InnerText;

                #region 写入记录
                if (!string.IsNullOrEmpty(model.FromUserName))
                {
                    var CheckIn = UserCore.CheckIsUser(model.FromUserName);
                    if (!CheckIn)//不存在则写入库中
                    {
                        UserCore.FirstInsert(model.FromUserName);
                    }
                }
                #endregion


                model.CreateTime = xmlnode["CreateTime"].InnerText;
                //model.MsgType = xmlnode["MsgType"].InnerText;
                model.MsgType = (RequestMsgType)System.Enum.Parse(typeof(RequestMsgType), xmlnode["MsgType"].InnerText, true);
                //Swagger.Models.Enum.Enum.RequestMsgType.


                switch (msgType)
                {
                    case "text":
                        model.MsgTypeName = "文本";
                        model.Content = xmlnode["Content"].InnerText;
                        model.MsgId = xmlnode["MsgId"].InnerText;
                        break;
                    case "image":
                        model.MsgTypeName = "图片";
                        model.PicUrl = xmlnode["PicUrl"].InnerText;
                        model.MediaId = xmlnode["MediaId"].InnerText;
                        model.MsgId = xmlnode["MsgId"].InnerText;
                        break;
                    case "voice":
                        model.MsgTypeName = "语音";
                        model.MediaId = xmlnode["MediaId"].InnerText;
                        model.Format = xmlnode["Format"].InnerText;
                        model.MsgId = xmlnode["MsgId"].InnerText;
                        break;
                    case "video":
                        model.MsgTypeName = "视频";
                        model.MediaId = xmlnode["MediaId"].InnerText;
                        model.ThumbMediaId = xmlnode["ThumbMediaId"].InnerText;
                        model.MsgId = xmlnode["MsgId"].InnerText;
                        break;
                    case "shortvideo":
                        model.MsgTypeName = "短视频";
                        model.MediaId = xmlnode["MediaId"].InnerText;
                        model.ThumbMediaId = xmlnode["ThumbMediaId"].InnerText;
                        model.MsgId = xmlnode["MsgId"].InnerText;
                        break;
                    case "location":
                        model.MsgTypeName = "地里位置";
                        model.Location_X = xmlnode["Location_X"].InnerText;
                        model.Location_Y = xmlnode["Location_Y"].InnerText;
                        model.Scale = xmlnode["Scale"].InnerText;
                        model.Label = xmlnode["Label"].InnerText;
                        model.MsgId = xmlnode["MsgId"].InnerText;
                        break;
                    case "link":
                        model.MsgTypeName = "链接";
                        model.Title = xmlnode["Title"].InnerText;
                        model.Description = xmlnode["Description"].InnerText;
                        model.Url = xmlnode["Url"].InnerText;
                        model.MsgId = xmlnode["MsgId"].InnerText;
                        break;
                    case "event":
                        model.Event = xmlnode["Event"].InnerText;
                        model.EventKey = xmlnode["EventKey"].InnerText;
                        break;
                    default:
                        break;
                }
                msg = model;
            }
            catch (Exception ex)
            {
                    msg = null;
                    LogCore.LogSave("解析微信消息失败", ex.Message, "500");
            }

            return msgType;
        }
        /// <summary>
        /// 获取wx发送消息类型，转化为model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ResponseMessage(WeiXinMsgModel model)
        {
            switch (model.MsgType)
            {
                case RequestMsgType.Text:
                    return ResponseMessageText.Message(model);
                case RequestMsgType.Image:
                    return ResponseMessageImage.Message(model);
                case RequestMsgType.Event:
                    if (model.Event == "subscribe")
                    {
                    }
                    return "";
            }
            return "";
        }
    }
}