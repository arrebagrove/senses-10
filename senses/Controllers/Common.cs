using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Groups;
using senses.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace senses.Controllers {
    public static class Common {

        private static LocalHueClient _hueClient;
        public static LocalHueClient hueClient {
            get {
                return _hueClient;
            }
            set {
                if (_hueClient != value) {
                    _hueClient = value;
                }
            }
        }

        private static string _appName = "senses";
        private static string _deviceName = "windowsmobile";

        public static string GetAppName() {
            return _appName;
        }

        public static string GetDeviceName() {
            return _deviceName;
        }

        private static bool _IsBridgeInRange { get; set; }

        public static bool IsBridgeInRange() {
            return _IsBridgeInRange;
        }

        public static bool IsRegistered { get; set; }

        public static ObservableCollection<SensesLight> Lights { get; set; }

        public static ObservableCollection<SensesGroup> Rooms { get; set; }

        private static async Task<LocalHueClient> GetBridge(string key) {
            IBridgeLocator locator = new HttpBridgeLocator();
            var bridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

            if (bridges == null) {
                // Didn't locate the bridge, send error response
                return null;
            }

            var bridgesArray = (List<string>)bridges;

            if (!string.IsNullOrEmpty(key)) {
                return new LocalHueClient(bridgesArray[0], key);
            }
            return new LocalHueClient(bridgesArray[0]);
        }

        private static async Task<bool> RegisterBridge() {
            if (IsBridgeInitialized()) {
                try {
                    var result = await hueClient.RegisterAsync(GetAppName(), GetDeviceName());
                    SettingsController.SaveBridgeKey(result);
                    IsRegistered = true;
                } catch (Exception ex) {
                    // Send a message that the bridge's access is not granted
                }
            }

            return IsRegistered;
        }

        public static async Task<bool> InitializeBridge() {
            // Check 1st if an secret key is avaible from app storage
            var key = SettingsController.RetrieveBridgeKey();

            hueClient = await GetBridge(key);            

            if (!string.IsNullOrEmpty(key)) {
                var check = await hueClient.CheckConnection();
                if (check) {
                    IsRegistered = true;
                    return check;
                }
            }
            
            return await RegisterBridge();            
        }

        public static bool IsBridgeInitialized() {
            return hueClient != null;
        }

        public static Dictionary<string, string> DictionnaryProductsIcons = new Dictionary<string, string>();


        public static async Task PopulateDictionnaryProductsIcons() {
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets\\Icons\\Products");
            var files = await assets.GetFilesAsync();

            string prefix = "Assets/Icons/Products/";

            foreach (StorageFile file in files) {
                DictionnaryProductsIcons.Add(file.Name.Replace(".png", ""), prefix + file.Name);
            }
        }

        public static async Task UpdateLightsState(List<string> lightsIds) {
            foreach (string id in lightsIds) {
                Light updatedLight = await hueClient.GetLightAsync(id);
                IEnumerable<SensesLight> results = Lights.
                        Where(light => light.Id == updatedLight.Id).
                        Select(x => { x.ObservableState = updatedLight.State; return x; }).ToList();
            }
        }

        public static async Task UpdateLightsNames(List<string> lightsIds) {
            foreach (string id in lightsIds) {
                Light updatedLight = await hueClient.GetLightAsync(id);
                IEnumerable<SensesLight> results = Lights.
                        Where(light => light.Id == updatedLight.Id).
                        Select(x => { x.ObservableName = updatedLight.Name; return x; }).ToList();
            }
        }

        public static async Task UpdateLights(List<string> lightsIds) {
            foreach (string id in lightsIds) {
                Light updatedLight = await hueClient.GetLightAsync(id);
                IEnumerable<SensesLight> results = Lights.
                        Where(light => light.Id == updatedLight.Id).
                        Select(x => {
                            x.ObservableState = updatedLight.State;
                            x.ObservableName = updatedLight.Name;
                            return x;
                        }).ToList();
            }
        }

        public static async Task UpdateRooms(List<string> roomsIds) {
            foreach (string id in roomsIds) {
                Group updatedGroup = await hueClient.GetGroupAsync(id);
                IEnumerable<SensesGroup> results = Rooms.
                    Where(room => room.Id == updatedGroup.Id).
                    Select(x => {
                        x.ObservableState = updatedGroup.State;
                        x.ObservableLights = updatedGroup.Lights;
                        x.ObservableName = updatedGroup.Name;
                        x.ObservableAction = updatedGroup.Action;
                        return x;
                    }).ToList();
            }
        }

        public static async Task UpdateRoomsNames(List<string> roomsIds) {
            foreach (string id in roomsIds) {
                Group updatedGroup = await hueClient.GetGroupAsync(id);
                IEnumerable<SensesGroup> results = Rooms.
                    Where(room => room.Id == updatedGroup.Id).
                    Select(x => {
                        x.ObservableName = updatedGroup.Name;
                        return x;
                    }).ToList();
            }
        }

        public static async Task UpdateRoomsStates(List<string> roomsIds) {
            foreach (string id in roomsIds) {
                Group updatedGroup = await hueClient.GetGroupAsync(id);
                IEnumerable<SensesGroup> results = Rooms.
                    Where(room => room.Id == updatedGroup.Id).
                    Select(x => {
                        x.ObservableState = updatedGroup.State;
                        return x;
                    }).ToList();
            }
        }

        public static async Task UpdateRoomsActions(List<string> roomsIds) {
            foreach (string id in roomsIds) {
                Group updatedGroup = await hueClient.GetGroupAsync(id);
                IEnumerable<SensesGroup> results = Rooms.
                    Where(room => room.Id == updatedGroup.Id).
                    Select(x => {
                        x.ObservableAction = updatedGroup.Action;
                        return x;
                    }).ToList();
            }
        }
    }
}
