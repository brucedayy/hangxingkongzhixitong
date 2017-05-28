using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace NC.Panel.Infrastructure.Extensions
{
    /// <summary>
    ///     获取属性或枚举描述的扩展方法。
    /// </summary>
    public static class DescriptionExt
    {
        /// <summary>
        ///     获取枚举类子项描述信息。
        /// </summary>
        /// <param name="enumSubitem">枚举类子项。</param>
        public static string GetEnumDescription(this Enum enumSubitem)
        {
            var strValue = enumSubitem.ToString();
            var fieldInfo = enumSubitem.GetType().GetField(strValue);
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                var da = (DescriptionAttribute)attributes[0];
                return da.Description;
            }
            return strValue;
        }
        
        /// <summary>
        ///     根据属性获取对应的属性名称。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <typeparam name="TK">对象数据的类型。</typeparam>
        /// <param name="t">对象。</param>
        /// <param name="expr">需要获取的属性。</param>
        /// <returns>属性名。</returns>
        public static string GetPropertyName<T, TK>(this T t, Expression<Func<T, TK>> expr)
        {
            //返回的属性名。
            var propertyName = string.Empty; 
            //对象是不是一元运算符  
            if (expr.Body is UnaryExpression)
                propertyName = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            //对象是不是访问的字段或属性  
            else if (expr.Body is MemberExpression)
                propertyName = ((MemberExpression)expr.Body).Member.Name;
            //对象是不是参数表达式  
            else if (expr.Body is ParameterExpression)
                propertyName = ((ParameterExpression)expr.Body).Type.Name;
            return propertyName;
        }
    }
}