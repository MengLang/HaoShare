using HaoShare.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HaoShare.WeiXin
{
    public class CheckSignature
    {
        /// <summary>
        /// 校验微信token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="postModel">需要提供：Timestamp、Nonce、Token</param>
        /// <returns></returns>
        public static bool Check(string signature, PostModel postModel)
        {
            return Check(postModel.Timestamp, postModel.Nonce, postModel.Token);
        }

        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Check(string signature, string timestamp, string nonce, string token = null)
        {
            return signature == GetSignature(timestamp, nonce, token);
        }

        /// <summary>
        /// 返回正确的签名
        /// </summary>
        /// <param name="postModel">需要提供：Timestamp、Nonce、Token</param>
        /// <returns></returns>
        public static string GetSignature(PostModel postModel)
        {
            return GetSignature(postModel.Timestamp, postModel.Nonce, postModel.Token);
        }

        /// <summary>
        /// 返回正确的签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetSignature(string timestamp, string nonce, string token = null)
        {
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            return enText.ToString();
        }
    }
}