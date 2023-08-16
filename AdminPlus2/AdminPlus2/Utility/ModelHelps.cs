using System.Reflection;

namespace AdminPlus2.Utility
{
    public class ModelHelps
    {
        public static void CopyProperties(object source, object destination)
        {
            // 获取类型信息
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            // 获取所有公共属性
            PropertyInfo[] sourceProperties = sourceType.GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                // 检查属性是否可读写
                if (sourceProperty.CanRead && sourceProperty.CanWrite)
                {
                    // 根据属性名称获取目标对象的属性
                    PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);

                    if (destinationProperty != null && destinationProperty.CanWrite)
                    {
                        // 获取源对象的属性值
                        object value = sourceProperty.GetValue(source);

                        // 设置目标对象的属性值
                        destinationProperty.SetValue(destination, value);
                    }
                }
            }
        }
    }
}
