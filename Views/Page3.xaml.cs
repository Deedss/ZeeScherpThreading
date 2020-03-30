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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZeeScherpThreading.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Page3 : Page
	{
		public Page3()
		{
			this.InitializeComponent();
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;
			resolution.SelectedValue = main.fractalgenerator.getTemplate().getWidth() + "x"+ main.fractalgenerator.getTemplate().getHeight();
			threads.SelectedValue = main.fractalgenerator.getNrOfThreads();
			color.Color = main.fractalgenerator.getColor();
		}

		private void resolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;
			var combo = sender as ComboBox;

			FractalTemplate.FractalTemplate f = main.fractalgenerator.getTemplate();

			String[] wh = combo.SelectedValue.ToString().Split('x');

			f.setWidth(Convert.ToInt32(wh[0]));
			f.setHeight(Convert.ToInt32(wh[1]));

		}

		private void threads_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;
			var combo = sender as ComboBox;

			main.fractalgenerator.setNrOfThreads(Convert.ToInt32(combo.SelectedValue));
		}

		private void color_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
		{
			var picker = sender as ColorPicker;
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			main.fractalgenerator.setColor(picker.Color);
		}
	}
}
