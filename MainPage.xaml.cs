using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace ZeeScherpThreading
{
    public sealed partial class MainPage : Page
    {
        public FractalGenerator fractalgenerator;
        public List<FractalTemplate.FractalTemplate> templateList;

        public MainPage()
        {
            this.InitializeComponent();

            fractalgenerator = new FractalGenerator();
            templateList = new List<FractalTemplate.FractalTemplate>();
            templateList.Add(new FractalTemplate.MandelBrot());
           // templateList.Add(new FractalTemplate.KockSnowflake());
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        // pageyboies
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>{
            ("Page1", typeof(Views.Page1)),
            ("Page2", typeof(Views.Page2)),
            ("Page3", typeof(Views.Page3)),
            ("Page4", typeof(Views.Page4)),
            ("Page5", typeof(Views.Page5)),
            ("Page6", typeof(Views.Page6)),
        };

        private void NavView_Loaded(object sender, RoutedEventArgs e){
            ContentFrame.Navigated += On_Navigated;

            NavView.SelectedItem = NavView.MenuItems[0];

            //default page optional
            NavView_Navigate("", new EntranceNavigationTransitionInfo());

            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            this.KeyboardAccelerators.Add(altLeft);
        }

        private void NavView_ItemInvoked(NavigationView sender,
                                         NavigationViewItemInvokedEventArgs args)
        {
            //if settings are needed
            if (args.IsSettingsInvoked == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            //rest van de items
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_SelectionChanged(NavigationView sender,
                                              NavigationViewSelectionChangedEventArgs args)
        {
            //voor settings
            if (args.IsSettingsSelected == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            //rest van de items
            else if (args.SelectedItemContainer != null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            if (navItemTag == "settings")
            {
                _page = typeof(Views.Settings);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }

            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }

           
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(Views.Settings))
            {
                //moet weer speciaal doen 
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }
    }
}
