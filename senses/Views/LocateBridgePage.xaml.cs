using senses.Controllers;
using System;
using System.Threading;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace senses.Views {
    public sealed partial class LocateBridgePage : Page {
        bool _IsBridgeRegistered { get; set; }
        int _MaxConnectionAttempts {
            get {
                return 6;
            }
        }

        int _ConnectionAttempts { get;set; }

        CoreDispatcher _UIDispatcher { get; set; }

        Timer _TimerSearch { get; set; }

        public LocateBridgePage() {
            InitializeComponent();
            Loaded += LocateBridgePage_Loaded;
        }

        private async void LocateBridgePage_Loaded(Object sender, RoutedEventArgs e) {
            //var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Icons/pushlink_bridgev2.svg"));
            //BridgeImage.LoadFileAsync(file);
            SearchBridgeRecurrently();

            ProgressConnection.Maximum = _MaxConnectionAttempts;
            _UIDispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) {
            //BridgeImage.SafeUnload();
            _TimerSearch.Dispose();
            _TimerSearch = null;
            _UIDispatcher = null;
        }

        private void SearchBridgeRecurrently() {
            var autoEvent = new AutoResetEvent(false);
            _TimerSearch = new Timer(CheckSearchStatus, autoEvent, 2000, 5000);

            //autoEvent.WaitOne();
            //_TimerSearch.Dispose();
        }

        private async void CheckSearchStatus(Object state) {
            AutoResetEvent autoEvent = (AutoResetEvent)state;
            if (_IsBridgeRegistered || _ConnectionAttempts > _MaxConnectionAttempts) {
                //autoEvent.Set();
                _TimerSearch.Dispose();
                IsBridgeRegistered();
                return;
            }

            _IsBridgeRegistered = await Common.InitializeBridge();
            _ConnectionAttempts++;
            UpdateProgressBar(_ConnectionAttempts, _MaxConnectionAttempts);
        }

        private async void UpdateProgressBar(int progression, int max) {
            _UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                ProgressConnection.Value = progression;

                if (progression == max) {
                    HideProgressUI();
                }
            });            
        }

        private void HideProgressUI() {
            ButtonStartAgain.Visibility = Visibility.Visible;
            TextBridgeNoFound.Visibility = Visibility.Visible;
            ProgressConnection.Visibility = Visibility.Collapsed;
        }

        private void ShowProgressUI() {
            ButtonStartAgain.Visibility = Visibility.Collapsed;
            TextBridgeNoFound.Visibility = Visibility.Collapsed;
            ProgressConnection.Visibility = Visibility.Visible;
        }

        private void IsBridgeRegistered() {
            if (Common.IsRegistered) {
                if (Frame.CanGoBack) {
                    Frame.GoBack();
                    return;
                }
                Frame.Navigate(typeof(HomePage));
            }
        }

        private void ButtonManualIP_Click(Object sender, RoutedEventArgs e) {
            _TimerSearch.Dispose();

            // Call the discovery bridge API with the bridge's IP
        }

        private void ButtonStartAgain_Click(Object sender, RoutedEventArgs e) {
            _TimerSearch.Dispose();

            _ConnectionAttempts = 0;
            ProgressConnection.Value = 0;

            ShowProgressUI();
            SearchBridgeRecurrently();
        }
    }
}
