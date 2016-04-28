using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoShare.Core.Business
{
    public class UserDetailsCore
    {
        private static HaoDBConStr context = new HaoDBConStr();
        public static void AddUserDetails(tUserDetails details)
        {
            context.tUserDetails.Add(details);
            context.SaveChangesAsync();

        }
        public static void UpdateUserDetails(tUserDetails old,tUserDetails details)
        {
            context.Entry(old).CurrentValues.SetValues(details);
            context.SaveChangesAsync();
        }
        public static tUserDetails QueryUserDetailByUID(int uid)
        {
           var result= context.tUserDetails.Single(d => d.UID == uid);
           return result;
        }
    }
}
