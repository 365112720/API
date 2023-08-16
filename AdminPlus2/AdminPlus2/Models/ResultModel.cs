namespace AdminPlus2.Models
{
    /// <summary>
    /// 定义返回状态
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = (int)ResultCode.OK;
        /// <summary>
        /// 数据
        /// </summary>
        public Object? data { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string? msg { get; set; }

    }

    /// <summary>
    /// 自定义状态码
    /// </summary>
    public enum ResultCode
    {
        OK = 200, // 200表示成功
        EEEOR = 500, //500表示错误
    }
}
