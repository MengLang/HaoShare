using HaoShare.Core;
using HaoShare.WeiXin.Core;
using HaoShare.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HaoShare.WeiXin.ResponseLogic
{
    public class ResponseMessageImage
    {
        private static readonly string ArticleImg = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[news]]></MsgType><ArticleCount>{3}</ArticleCount><Articles>{4}</Articles></xml>";
        private static readonly int articleCount = 2;//文章数默认
        private static readonly string Title = "本次上传的图片|我的历史图片",Description="本次图片|历史图片";
        private static readonly string Key = HConfig.Key;
        public static string Message(WeiXinMsgModel model)
        {
            if (model != null&&model.MsgType==RequestMsgType.Image)
            {
                string rcPWD = RC4Crypto.Encrypt(Key, model.FromUserName);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < articleCount; i++)
                {
                    sb.Append("<item>");
                    sb.Append("<Title>");
                    sb.Append(string.Format("<![CDATA[{0}]]>",Title.Split('|')[i]));
                    sb.Append("</Title>");
                    sb.Append("<Description>");
                    sb.Append(string.Format("<![CDATA[{0}]]>",Description.Split('|')[i]));
                    sb.Append("</Description>");
                    sb.Append("<PicUrl>");
                    sb.Append(string.Format("<![CDATA[{0}]]>",model.PicUrl));
                    sb.Append("</PicUrl>");
                    sb.Append("<Url>");
                    sb.Append(string.Format("<![CDATA[{0}]]>", "http://www.oazjg.com/WeiXin/Photo/?ST=" + rcPWD));
                    sb.Append("</Url>");
                    sb.Append("</item>");
                }
                sb.ToString();
                
                string Content = string.Format(ArticleImg, model.FromUserName, model.ToUserName, DateTime.Now.Ticks,articleCount, sb);
                SaveLoadImgForAsync(model.PicUrl);
                return Content;
            }
            return "success";
        }

        /// <summary>
        /// 保存用户上传图片
        /// </summary>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        private static string SaveLoadImg(string picUrl,string openID=null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(picUrl);
            request.Method = "GET";
            request.Timeout = 30000;
            request.AllowAutoRedirect = true;
            request.ContentType = "image/bmp";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:11.0) Gecko/20100101 Firefox/11.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
           
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Bitmap sourcebm = new Bitmap(sr.BaseStream);
                sr.Close();
                ImageFormat format = sourcebm.RawFormat;
                var SavePath = "~/Content/UserImg/" + System.Guid.NewGuid() + ".";
                if (format.Guid.Equals(ImageFormat.Jpeg.Guid))
                {
                    SavePath = SavePath + ImageFormat.Jpeg;
                    sourcebm.Save(HttpContext.Current.Server.MapPath(SavePath));
                    sourcebm.Dispose();
                }
                if (format.Guid.Equals(ImageFormat.Png.Guid))
                {
                    SavePath = SavePath + ImageFormat.Png;
                    sourcebm.Save(HttpContext.Current.Server.MapPath(SavePath));
                    sourcebm.Dispose();
                }
                if (format.Guid.Equals(ImageFormat.Gif.Guid))
                {
                    SavePath = SavePath + ImageFormat.Gif;
                    sourcebm.Save(HttpContext.Current.Server.MapPath(SavePath));
                    sourcebm.Dispose();
                }
                return  SavePath;
            }
        }

        private static string SaveLoadImgForAsync(string picUrl, string openID = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(picUrl);
                request.Method = "GET";
                request.Timeout = 30000;
                request.AllowAutoRedirect = true;
                request.ContentType = "image/bmp";
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:11.0) Gecko/20100101 Firefox/11.0";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                var ws = request.GetResponseAsync();//.ConfigureAwait(true);

                StreamReader sr = new StreamReader(ws.GetAwaiter().GetResult().GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Bitmap sourcebm = new Bitmap(sr.BaseStream);
                sr.Close();
                ImageFormat format = sourcebm.RawFormat;
                string SavePath = "~/Content/UserImg/" + System.Guid.NewGuid() + ".";
                if (format.Guid.Equals(ImageFormat.Jpeg.Guid))
                {
                    SavePath = SavePath + ImageFormat.Jpeg;
                    sourcebm.Save(HttpContext.Current.Server.MapPath(SavePath));
                    sourcebm.Dispose();
                }
                if (format.Guid.Equals(ImageFormat.Png.Guid))
                {
                    SavePath = SavePath + ImageFormat.Png;
                    sourcebm.Save(HttpContext.Current.Server.MapPath(SavePath));
                    sourcebm.Dispose();
                }
                if (format.Guid.Equals(ImageFormat.Gif.Guid))
                {
                    SavePath = SavePath + ImageFormat.Gif;
                    sourcebm.Save(HttpContext.Current.Server.MapPath(SavePath));
                    sourcebm.Dispose();
                }
                return SavePath;
            }
            catch (Exception ex)
            {
                //Log.LogLogic.SaveLog("~Log/Error/" ,"saveimg" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".txt", ex.Message);
                return "";
            }
        }
    }
}