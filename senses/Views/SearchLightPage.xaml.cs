using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace senses.Views {
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SearchLightPage : Page {
        public SearchLightPage() {
            this.InitializeComponent();
            Loaded += SearchLightPage_Loaded;
        }

        private void SearchLightPage_Loaded(Object sender, RoutedEventArgs e) {
            BoxDeviceId.Focus(FocusState.Programmatic);
        }

        private void SearchButton_Click(Object sender, RoutedEventArgs e) {

        }
    }
}
