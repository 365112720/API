using Models;

namespace AdminPlus2.Utility
{
    public class UserHelps
    {
        /// <summary>
        /// 判断用户名和密码是否为空-----判断密码长度等也是在这里判断
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public static bool CheckUser(User newUser)
        {
            return newUser == null || string.IsNullOrEmpty(newUser.Username) || string.IsNullOrEmpty(newUser.Password);
        }
    }
}
