using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace P001_CustomEnumComboBoxEditor.Editor
{
    /// <summary>
    ///     Редактор для свойств типа перечисления
    /// </summary>
    /// <remarks>
    ///     Отображает выпадающий список, элементами которого являются члены перечисления.
    ///     При этом проверяется атрибут Browsable и Description:
    ///     - если значение атрибута Browsable равно false, элемент отображаться не будет;
    ///     - если указан атрибут Description, то отобразится его значение.
    /// </remarks>
    public class CustomEnumComboBoxEditor : ComboBoxEditor
    {
        protected override IValueConverter CreateValueConverter()
        {
            return GetConverter();
        }

        private IValueConverter GetConverter()
        {
            return new EnumDescriptionConverter();
        }

        protected override IEnumerable CreateItemsSource(PropertyItem propertyItem)
        {
            return GetValues(propertyItem.PropertyType);
        }

        private IEnumerable GetValues(Type enumType)
        {
            var values = new List<string>();

            if (enumType != null)
            {
                var fields = enumType.GetFields().Where(x => x.IsLiteral);

                foreach (var field in fields)
                {
                    var browsableAttributes = field.GetCustomAttributes(typeof(BrowsableAttribute), false);

                    if (browsableAttributes.Length == 1)
                    {
                        var browsableAttribute = (BrowsableAttribute)browsableAttributes[0];

                        if (browsableAttribute.Browsable == false)
                        {
                            continue;
                        }
                    }

                    var descriptionAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (descriptionAttributes.Length == 1)
                    {
                        var descriptionAttribute = (DescriptionAttribute)descriptionAttributes[0];
                        values.Add(descriptionAttribute.Description);

                        continue;
                    }

                    values.Add(field.Name);
                }
            }

            return values.ToArray();
        }
    }
}
