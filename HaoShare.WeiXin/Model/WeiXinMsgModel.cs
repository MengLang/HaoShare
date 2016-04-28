using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HaoShare.WeiXin.Model
{
    /// <summary>
    /// 接收微信消息model
    /// </summary>
    public class WeiXinMsgModel
    {
        /// <summary>
        /// 接收方
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型 text/image/voice/video/shortvideo/location/link/event
        /// </summary>
        public RequestMsgType MsgType { get; set; }
        /// <summary>
        /// 消息类型名称
        /// </summary>
        public string MsgTypeName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 语音格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 视频媒体id
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 媒体id
        /// </summary>
        public string MediaId { get; set; }
        #region 地图信息
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
        #endregion

        #region 链接信息
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }

        #endregion
        /// <summary>
        /// 消息id
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// subscribe(订阅)、unsubscribe(取消订阅)
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 源数据
        /// </summary>
        public string SourceXml { get; set; }
    }
}