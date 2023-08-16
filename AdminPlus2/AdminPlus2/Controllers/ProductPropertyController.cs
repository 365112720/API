using AdminPlus2.DTO;
using AdminPlus2.Models;
using AdminPlus2.Utility;
using AdminXP.Utility.SwaggerExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ShopInterface;
using ShopService;

namespace AdminPlus2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]


    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.商品信息管理))]  //显示效果
    public class ProductPropertyController : ControllerBase
    {
        private readonly ILogger<ProductPropertyController> _logger;

        private readonly DbContext _context;

        private readonly IProductPropertyService _propertyService;

        public ProductPropertyController(ILogger<ProductPropertyController> logger, DbContext context, IProductPropertyService productPropertyService)
        {
            _logger = logger;
            this._context=context;

            _propertyService = productPropertyService;

        }

        [HttpPost()]
        public ResultModel AddProductProperty([FromForm] AddProductPropertyDTO addProductPropertyDTO)
        {
            ResultModel result = new ResultModel();
            try
            {
                //判断数据是否为空
                if (ProductPropertyHelps.CheckProductProperty(addProductPropertyDTO))
                {
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "用户名或密码为空";
                    return result;
                }
                //其它校验---我这里不写了

                //判断是否上传有图片
                if (addProductPropertyDTO.File == null || addProductPropertyDTO.File.Length == 0)
                {
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "上传的文件不能为空";
                    return result;
                }
                //判断上传的文件是否是图片
                bool isImage = ProductPropertyHelps.IsImage(addProductPropertyDTO.File);
                if (!isImage)
                {
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "上传的文件不是图片";
                    return result;
                }

                //保存图片
                string uniqueFileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + " " + addProductPropertyDTO.File.FileName;
                //保存图片路径
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Imges");
                //判断文件夹是否存在
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //拼接上文件名称
                filePath = Path.Combine(filePath, uniqueFileName);
                //保存文件
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addProductPropertyDTO.File.CopyTo(fileStream);
                }
                //产品属性对象
                ProductProperty productProperty = new ProductProperty();
                ModelHelps.CopyProperties(addProductPropertyDTO, productProperty);
                productProperty.Image = filePath;//保存图片路径
                //保存产品属性对象
                _propertyService.Insert<ProductProperty>(productProperty);

                result.msg = "保存产品对象成功";
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }
            return result;
        }

        // GET: api/<ProductPropertyController>
        [HttpGet]
        public ResultModel GetAllProductProperty()
        {
            ResultModel result = new ResultModel();
            try
            {
                //查询所有
                result.data = _propertyService.Set<ProductProperty>();
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }
            return result;
        }

    }
}
