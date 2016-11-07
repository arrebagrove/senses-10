using senses.Controllers;
using System;
using Windows.UI.Xaml.Data;

namespace senses.Converters {
    public class ProductstIcon : IValueConverter {
        Object IValueConverter.Convert(Object value, Type targetType, Object parameter, String language) {
            string product = (string)value;
            string path;
            Common.DictionnaryProductsIcons.TryGetValue(product, out path);

            if (path != null) {
                return "ms-appx:///" + path;
            }

            return null;
        }

        Object IValueConverter.ConvertBack(Object value, Type targetType, Object parameter, String language) {
            throw new NotImplementedException();
        }
    }
}
