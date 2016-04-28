using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HaoShare.Core
{
    public class HConfig
    {
        /// <summary>
        /// Token
        /// </summary>
        public  static  string Token = ConfigurationManager.AppSettings["wxToken"];
        /// <summary>
        /// 编码Key
        /// </summary>
        public static  string EncodingAESKey = ConfigurationManager.AppSettings["wxEncodingAESKey"];
        /// <summary>
        /// APPID
        /// </summary>
        public static string AppId = ConfigurationManager.AppSettings["wxAppID"];
        /// <summary>
        /// 加密Key
        /// </summary>
        public static string Key = ConfigurationManager.AppSettings["rcKey"];
        //readonly Func<string> _getRandomFileName = () => DateTime.Now.ToString("yyyyMMdd-HHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);
    }
}