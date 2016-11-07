using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace senses.Converters {
    public class RoomBackground : IValueConverter {
        Object IValueConverter.Convert(Object value, Type targetType, Object parameter, String language) {
            switch (value) {
                case Q42.HueApi.Models.Groups.RoomClass.Bathroom:
                    return "ms-appx:///Assets/Icons/towel.png";
                case Q42.HueApi.Models.Groups.RoomClass.FrontDoor:
                    return "ms-appx:///Assets/Icons/door.png";
                case Q42.HueApi.Models.Groups.RoomClass.LivingRoom:
                    return "ms-appx:///Assets/Icons/armchair.png";
                case Q42.HueApi.Models.Groups.RoomClass.Kitchen:
                    return "ms-appx:///Assets/Icons/coffeemaker.png";
                default:
                    return "";
            }
        }

        Object IValueConverter.ConvertBack(Object value, Type targetType, Object parameter, String language) {
            throw new NotImplementedException();
        }
    }
}
