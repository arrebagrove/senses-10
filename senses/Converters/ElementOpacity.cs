using System;
using Windows.UI.Xaml.Data;

namespace senses.Converters {
    public class ElementOpacity : IValueConverter {
        public Object Convert(Object value, Type targetType, Object parameter, String language) {
            if ((bool)value == true) {
                return 1;
            }

            return 0.5;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, String language) {
            throw new NotImplementedException();
        }
    }
}
