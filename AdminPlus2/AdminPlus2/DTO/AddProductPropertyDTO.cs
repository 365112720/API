using System.ComponentModel.DataAnnotations;

namespace AdminPlus2.DTO
{
    public class AddProductPropertyDTO
    {
        public int ProperID { get; set; }

        public string? ProperName { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public IFormFile File { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int TypeID { get; set; }

        public string? Description { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
