using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Groups;
using senses.Controllers;
using senses.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace senses.Views {
    public sealed partial class HomePage : Page {
        private HomeController controller { get; set; }

        public HomePage() {
            InitializeComponent();
            Loaded += HomePage_Loaded;
        }

        private async void HomePage_Loaded(Object sender, RoutedEventArgs e) {
            bool isRegistered = await Common.InitializeBridge();

            if (!isRegistered) {
                Frame.Navigate(typeof(LocateBridgePage), null, new DrillInNavigationTransitionInfo());
                return;
            }

            await Common.PopulateDictionnaryProductsIcons();

            await FindRooms();
            await FindLights();

            CheckLoadedData();
        }

        private async Task FindLights() {
            var lights = await Common.hueClient.GetLightsAsync();
            Common.Lights = new ObservableCollection<SensesLight>();
            LightsView.ItemsSource = Common.Lights;

            foreach (Light light in lights) {
                SensesLight sensesLight = new SensesLight(light);
                Common.Lights.Add(sensesLight);
            }
            
        }

        private async Task FindRooms() {
            var rooms = await Common.hueClient.GetGroupsAsync();
            Common.Rooms = new ObservableCollection<SensesGroup>();
            RoomsView.ItemsSource = Common.Rooms;

            foreach (Group room in rooms) {
                SensesGroup sensesRoom = new SensesGroup(room);
                Common.Rooms.Add(sensesRoom);
            }
        }

        private void CheckLoadedData() {
            if (Common.Lights.Count > 0 || Common.Rooms.Count>0) {
                PanelLoading.Visibility = Visibility.Collapsed;
                PanelData.Visibility = Visibility.Visible;
            }
        }

        private void NewLightManually_Click(Object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(SearchLightPage), null, new DrillInNavigationTransitionInfo());
        }

        private void BlinkAndExpand_Tap(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
            Grid grid = (Grid)sender;
            List<string> lights = new List<string>();

            if (typeof(SensesLight) == grid.DataContext.GetType()) {
                var light = (SensesLight)grid.DataContext;
                var id = light.Id;
                lights.Add(id);
            } else if (typeof(SensesGroup) == grid.DataContext.GetType()) {
                SensesGroup group = (SensesGroup)grid.DataContext;
                lights.AddRange(group.Lights);
            }

            var blink = new LightCommand();
            blink.Alert = Alert.Once;
            Common.hueClient.SendCommandAsync(blink, lights);
        }

        private void ToggleONOFF_Tap(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
            e.Handled = true; // avoid firing BlinkAndExpand_Tap event
            Button button = (Button)sender;

            var lights = GetLights(button);
            var ONOFFCommand = new LightCommand();
            bool isOn = false;

            if (typeof(SensesGroup) == button.DataContext.GetType()) {
                Group group = (SensesGroup)button.DataContext;
                isOn = group.State.AllOn == true ? true : false;

            } else if (typeof(SensesLight) == button.DataContext.GetType()) {
                SensesLight light = (SensesLight)button.DataContext;
                isOn = light.State.On;
            }

            ONOFFCommand.On = !isOn;
            Common.hueClient.SendCommandAsync(ONOFFCommand, lights);
            Common.UpdateLightsState(lights);
        }

        private List<string> GetLights(FrameworkElement element) {
            List<string> lights = new List<string>();

            if (typeof(SensesLight) == element.DataContext.GetType()) {
                var light = (SensesLight)element.DataContext;
                var id = light.Id;
                lights.Add(id);
            } else if (typeof(SensesGroup) == element.DataContext.GetType()) {
                SensesGroup group = (SensesGroup)element.DataContext;
                lights.AddRange(group.Lights);
            }

            return lights;
        }
    }
}
