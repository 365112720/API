using Models;

namespace AdminPlus2.Utility
{
    public class CustomerHelps
    {
        /// <summary>
        /// 判断用户名和密码是否为空-----判断密码长度等也是在这里判断
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public static bool CheckCustomer(Customer newCustomer)
        {
            return newCustomer == null || string.IsNullOrEmpty(newCustomer.Account) || string.IsNullOrEmpty(newCustomer.Password);
        }
    }
}
