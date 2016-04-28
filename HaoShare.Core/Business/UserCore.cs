using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoShare.Core.Business
{
    public class UserCore
    {
        private static HaoDBConStr context = new HaoDBConStr();
        /// <summary>
        /// 添加用户返回用户id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static int AddUser(tUsers user)
        {
            try
            {
                context.tUsers.Add(user);
                context.SaveChanges();
                int ID = user.ID;
                tUserDetails details = new tUserDetails()
                {
                    UID = ID,
                    fName = "",
                    fAddress = "",
                    fMobile = "",
                    fHeadImg = "",
                    fNickName = "",
                    fStatus = 0,
                    fSex = "",
                    fCreateDate = DateTime.Now,
                    fUpdateDate = DateTime.Now,
                    fUpdateIp = "",
                    fWeiXinName = "",
                };
                context.tUserDetails.Add(details);
                context.SaveChanges();
                return user.ID;
            }
            catch (Exception ex)
            {
                LogCore.LogSave("添加新用户记录失败", ex.Message, "500");
                return 0;
            }
        }

        /// <summary>
        /// 新用户注册
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public static int FirstInsert(string openID)
        {
            var random = new Random();
            tUsers entity = new tUsers()
            {
                fWeiXinID = openID,
                fIsLock = false,
                fIsBind = 0,
                fRemark = "",
                fStatus = 0,
                fLoginID = "",
                fPassword = random.Next(10000, 99999).ToString(),//随机生成密码
                fUpdateIp = "127.0.0.1",
                fCreateDate = DateTime.Now,
                fUpdateDate = DateTime.Now,
            };
            UserCore.AddUser(entity);
            return entity.ID;
        }
        /// <summary>
        /// 校验用户OpenID是否存在
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public static bool CheckIsUser(string OID)
        {
            var result = context.tUsers.Where(d => d.fWeiXinID == OID).ToList();
            if (result.Count > 0)
                return true;
            else
                return false;
        }
        public static tUsers QueryUser(int ID)
        {
           return context.tUsers.Single(d => d.ID == ID);
        }
        public static tUsers QueryUserByOID(string wxID)
        {
            return context.tUsers.Single(d => d.fWeiXinID == wxID);
        }
    }
}
