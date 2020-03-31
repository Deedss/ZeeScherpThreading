using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZeeScherpThreading.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Add : Page
    {
        public Add()
        {
            this.InitializeComponent();
        }

        private void Name_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                Debug.WriteLine("Oh lordy");
                var frame = (Frame)Window.Current.Content;
                var main = (MainPage)frame.Content;
                FractalTemplate.FractalTemplate f;
                
                if (Type.SelectedIndex == 1)
                {
                    f = new FractalTemplate.JuliaSet();
                    f.name = Name.Text;
                    main.fractaleditor.GetFractals().Add(f);
                }
                if (Type.SelectedIndex == 0)
                {
                    f = new FractalTemplate.MandelBrot();
                    f.name = Name.Text;
                    main.fractaleditor.GetFractals().Add(f);
                }
                main.NavView_Navigate("Page2", new EntranceNavigationTransitionInfo());

            }
        }
    }
}
