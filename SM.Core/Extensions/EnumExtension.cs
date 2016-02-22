using System;
using System.Linq;
using System.Reflection;
using SM.Contracts.Attributes;

namespace SM.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum e)
        {
            var type = e.GetType();            
            if (type.GetCustomAttribute(typeof (CodeAttribute)) == null)
            {
                return string.Empty;
            }
            
            var enumValue = type.GetMember(e.ToString());

            var value = enumValue.FirstOrDefault();
            if (value == null)
            {
                return string.Empty;
            }

            var attributes = value.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var attribute = attributes.FirstOrDefault();           

            if (attribute == null)
            {
                return string.Empty;
            }

            var descriptionAttribute = attribute as DescriptionAttribute;
            return descriptionAttribute != null ? descriptionAttribute.Description : string.Empty;
        }
    }
}