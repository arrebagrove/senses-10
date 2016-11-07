using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace senses.Controllers {
    public static class SettingsController {

        public static bool SaveBridgeKey(string key) {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["BridgeKey"] = key;
            return true;
        }

        public static string RetrieveBridgeKey() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            return (string)localSettings.Values["BridgeKey"];
        }
    }
}
