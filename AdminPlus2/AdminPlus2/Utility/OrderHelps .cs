using Models;

namespace AdminPlus2.Utility
{
    public class OrderHelps
    {
        /// <summary>
        /// 判断数据字段是否为空-----判长度等也是在这里判断
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool CheckOrder(Order order)
        {
            return order == null || string.IsNullOrEmpty(order.OrderState) || (order.OrderMoney  <0 && order.OrderMoney > 1000
 || string.IsNullOrEmpty(order.SenDate)|| string.IsNullOrEmpty(order.RecevieDate)|| string.IsNullOrEmpty(order.AddressInfo) ||string.IsNullOrEmpty(order.InvoiceName)
 || string.IsNullOrEmpty(order.InvoiceType)|| (order.Postage<0&&order.Postage>200000)||string.IsNullOrEmpty(order.Express)||string.IsNullOrEmpty(order.ExpressNumber));
        }
    }
}
