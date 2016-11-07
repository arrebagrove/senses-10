using senses.Presentation;
using senses.Views;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace senses {
    public sealed partial class Shell : UserControl {
        private static string _header;
        public string Header {
            get {
                if (_header == null) {
                    _header = "Senses";
                }
                return _header;
            }
            set {
                if (_header != value) {
                    _header = value;
                }
            }
        }

        public Shell() {
            this.InitializeComponent();

            var vm = new ShellViewModel();

            vm.MenuItems.Add(new MenuItem {
                Icon = "",
                SymbolAsChar = '\uEA8A',
                Label = "Home",
                PageType = typeof(HomePage)
            });
            //vm.MenuItems.Add(new MenuItem {
            //    Icon = "",
            //    SymbolAsChar = '\uE74C',
            //    Label = "wincomposition",
            //    PageType = typeof(WincompositionPage)
            //});
            //vm.MenuItems.Add(new MenuItem {
            //    Icon = "",
            //    SymbolAsChar = '\uE179',
            //    Label = "ListView",
            //    PageType = typeof(ListPage)
            //});
            //vm.MenuItems.Add(new MenuItem {
            //    Icon = "",
            //    SymbolAsChar = '\uE81C',
            //    Label = "Tasks",
            //    PageType = typeof(TasksPage)
            //});
            //vm.MenuItems.Add(new MenuItem {
            //    Icon = "",
            //    SymbolAsChar = '\uE713',
            //    Label = "About",
            //    PageType = typeof(AboutPage)
            //});

            // select the first menu item
            vm.SelectedMenuItem = vm.MenuItems.First();

            this.ViewModel = vm;

            // add entry animations
            var transitions = new TransitionCollection { };
            var transition = new NavigationThemeTransition { };
            transitions.Add(transition);
            this.Frame.ContentTransitions = transitions;
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame {
            get {
                if (Frame.SourcePageType != null) {
                    SetPageHeaderTitle(Frame.SourcePageType.Name);
                }
                return Frame;
            }
        }

        /// <summary>
        /// Set the related header when loading to a page
        /// </summary>
        /// <param name="name">page name</param>
        private void SetPageHeaderTitle(string name) {
            switch (name) {
                case "HomePage":
                    VisualHeader.Text = "Home";
                    break;
                case "WincompositionPage":
                    VisualHeader.Text = "WinComposition Samples";
                    break;
                case "ListPage":
                    VisualHeader.Text = "ListView Samples";
                    break;
                case "TasksPage":
                    VisualHeader.Text = "Tasks Samples";
                    break;
                default:
                    VisualHeader.Text = name;
                    break;
            }
        }

        /// <summary>
        /// Set a custom header
        /// </summary>
        /// <param name="name">custom header's name</param>
        public void SetHeaderTitle(string name) {
            VisualHeader.Text = name;
        }

        //private async void VisualHeader_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
        //    var listView = App._shell.GetChildOfType<ListViewBase>();

        //    if (listView == null) {
        //        return;
        //    }

        //    await VisualTreeExtensions.ScrollToIndex(listView, 0);
        //}
    }
}
