﻿using System;
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
        public FractalGenerator fractalgenerator = new FractalGenerator();
        public FractalEditor fractaleditor = new FractalEditor();
        public MainPage()
        {
            this.InitializeComponent();
            //Add default templates to editor when no file is loaded by user
            fractaleditor.GetFractals().Add(new FractalTemplate.MandelBrot());
            fractaleditor.GetFractals().Add(new FractalTemplate.JuliaSet());

            fractalgenerator.setTemplate(fractaleditor.GetFractals()[0]);
        }

        /// <summary>
        /// When sidebar navigation fails
        /// </summary>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        // pages in sidebar
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>{
            ("Page1", typeof(Views.DrawFractal)),
            ("Page2", typeof(Views.SelectFractal)),
            ("Page3", typeof(Views.EditFractal)),
            ("Add", typeof(Views.Add)),
            ("About", typeof(Views.About)),
        };

        /// <summary>
        /// When sidebar is loaded
        /// </summary>
        private void NavView_Loaded(object sender, RoutedEventArgs e){
            ContentFrame.Navigated += On_Navigated;

            NavView.SelectedItem = NavView.MenuItems[0];

            // set default page blank
            NavView_Navigate("", new EntranceNavigationTransitionInfo());

            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            this.KeyboardAccelerators.Add(altLeft);
        }

        /// <summary>
        /// When sidebar item is pressed
        /// </summary>
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

        /// <summary>
        /// Navigate to sidebar item page
        /// </summary>
        public void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo, Boolean force = false)
        {
            Type _page = null;
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            _page = item.Page;

            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!(_page is null)  && !Type.Equals(preNavPageType, _page) || force)
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        /// <summary>
        /// When changing page is complete
        /// </summary>
        private void On_Navigated(object sender, NavigationEventArgs e)
        {
           NavView.IsBackEnabled = ContentFrame.CanGoBack;
           if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        /// <summary>
        /// Load file sidebar item clicked
        /// </summary>
        private async void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".fs");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                await fractaleditor.LoadFractals(file);
                //Goto fractal select page
                NavView_Navigate("Page2", new EntranceNavigationTransitionInfo(), true);
            }
        }

        /// <summary>
        /// Save file sidebar item clicked
        /// </summary>
        private async void NavigationViewItem_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("FractaalSamensteller bestand", new List<string>() { ".fs" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "Fractals";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            fractaleditor.SaveFractals(file);
        }
    }
}
