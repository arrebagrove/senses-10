using System;
using Windows.UI.Xaml.Data;
using Q42.HueApi.ColorConverters.OriginalWithModel;

namespace senses.Converters {
    public class LightColorIcon : IValueConverter {
        Object IValueConverter.Convert(Object value, Type targetType, Object parameter, String language) {
            Q42.HueApi.State state = (Q42.HueApi.State)value;

            if (state.On == false || state.IsReachable == false) {
                return "#2c3e50"; // midnight blue
            }

            if (state.ColorCoordinates != null) {
                return "#" + state.ToHex();
            }

            return "#ecf0f1"; // white clouds
        }

        Object IValueConverter.ConvertBack(Object value, Type targetType, Object parameter, String language) {
            throw new NotImplementedException();
        }
    }
}
