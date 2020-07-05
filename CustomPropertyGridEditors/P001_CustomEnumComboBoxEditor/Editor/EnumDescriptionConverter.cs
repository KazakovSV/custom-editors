using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace P001_CustomEnumComboBoxEditor.Editor
{
    public class EnumDescriptionConverter : IValueConverter
    {
        private Type enumType;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            enumType = value.GetType();
            var result = GetDescriptionOrName(value);

            return result;
        }

        private string GetDescriptionOrName(object value)
        {
            var fieldInfo = enumType.GetField(Enum.GetName(enumType, value));
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

            return descriptionAttribute == null ? value.ToString() : descriptionAttribute.Description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var fieldInfo in enumType.GetFields())
            {
                var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

                if ((descriptionAttribute != null) && (value.ToString() == descriptionAttribute.Description))
                {
                    return Enum.Parse(enumType, fieldInfo.Name);
                }
            }

            return Enum.Parse(enumType, value.ToString());
        }
    }
}
