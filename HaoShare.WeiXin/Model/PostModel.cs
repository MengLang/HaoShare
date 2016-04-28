using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoShare.WeiXin.Model
{
    /// <summary>
    /// 校验微信消息model
    /// </summary>
    [Serializable]
    public class PostModel
    {
        public string Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Token { get; set; }
        //{
        //    get { return  ConfigurationManager.AppSettings["wxToken"]; }
        //    set { Token = null; }
        //}
        public string EncodingAESKey { get; set; }
        //{
        //    get { return ConfigurationManager.AppSettings["wxEncodingAESKey"]; }
        //    set { EncodingAESKey = null; }
        //}
        public string AppId { get; set; }
        //{
        //    get { return ConfigurationManager.AppSettings["wxAppID"]; }
        //    set { AppId = null; }
        //}
    }
}
