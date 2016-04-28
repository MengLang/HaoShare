using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoShare.Core.Business
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogCore
    {
        private static HaoDBConStr context = new HaoDBConStr();
        private static void LogSave(tLog log)
        {
            context.tLog.Add(log);
            context.SaveChanges();
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Message"></param>
        /// <param name="fLevel"></param>
        public static void LogSave(string Name, string Message, string Code)
        {
            tLog log = new tLog()
            {
                fLevel = "",
                fMessage = Message,
                fName = Name,
                fCreateDate = DateTime.Now,
                fCode=Code,
                fIP = ""
            };
            LogSave(log);
        }
    }
}
