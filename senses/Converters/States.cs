using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace senses.Converters {
    public class States : IValueConverter {
        public Object Convert(Object value, Type targetType, Object parameter, String language) {
            string text = (bool)value == true ? "ON" : "OFF";
            return text;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, String language) {
            throw new NotImplementedException();
        }
    }
}
