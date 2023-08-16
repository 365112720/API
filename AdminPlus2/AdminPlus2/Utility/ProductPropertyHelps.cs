using AdminPlus2.DTO;
using Models;

namespace AdminPlus2.Utility
{
    public class ProductPropertyHelps
    {
        /// <summary>
        /// 判断数据是否为空
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public static bool CheckProductProperty(AddProductPropertyDTO addProductPropertyDTO)
        {
            if (addProductPropertyDTO == null)
            {
                return true;
            }
            if (string.IsNullOrEmpty(addProductPropertyDTO.ProperName))
            {
                return true;
            }
            if (addProductPropertyDTO.Price<0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断文件是否是图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsImage(IFormFile file)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            // 判断文件扩展名是否为图片格式
            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }

            // 判断文件的 MIME 类型是否以 "image/" 开头
            if (!file.ContentType.StartsWith("image/"))
            {
                return false;
            }

            return true;
        }
    }
}
