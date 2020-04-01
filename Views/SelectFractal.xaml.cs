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
	public sealed partial class SelectFractal : Page
	{
		public SelectFractal()
		{
			this.InitializeComponent();
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			foreach (FractalTemplate.FractalTemplate template in main.fractaleditor.GetFractals())
			{
				FractalBox.Items.Add(template.name);
			}

			if (main.fractalgenerator.getTemplate() != null)
			{
				FractalBox.SelectedItem = main.fractalgenerator.getTemplate().name;
			}
			else
			{
				FractalBox.SelectedIndex = 0;
			}
		}
		/// <summary>
		/// When combobox value is changed by user
		/// </summary>
		private void FractalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;
			var combo = sender as ComboBox;
	
			main.fractalgenerator.setTemplate(main.fractaleditor.GetFractals()[combo.SelectedIndex]);
			main.NavView_Navigate("Page3", new EntranceNavigationTransitionInfo());
		}
	}
}
