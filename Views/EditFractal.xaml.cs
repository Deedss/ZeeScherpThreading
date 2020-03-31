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
	public sealed partial class EditFractal : Page
	{
		public EditFractal()
		{
			this.InitializeComponent();
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			//Load if user selected value else load first item in list
			if(main.fractalgenerator.getTemplate().getWidth() != 0 && main.fractalgenerator.getTemplate().getHeight() != 0)
			{
				resolution.SelectedValue = main.fractalgenerator.getTemplate().getWidth() + "x" + main.fractalgenerator.getTemplate().getHeight();
			}
			else
			{
				resolution.SelectedIndex = 0;
			}

			//Load if user selected value else load first item in list
			if (main.fractalgenerator.getTemplate().getNrOfThreads() != 0)
			{
				threads.SelectedValue = main.fractalgenerator.getTemplate().getNrOfThreads();
			}
			else
			{
				threads.SelectedIndex = 0;
			}
			
			color.Color = main.fractalgenerator.getTemplate().getColor();
			x1.Text = main.fractalgenerator.getTemplate().x1.ToString();
			x2.Text = main.fractalgenerator.getTemplate().x2.ToString();
			y1.Text = main.fractalgenerator.getTemplate().y1.ToString();
			y2.Text = main.fractalgenerator.getTemplate().y2.ToString();
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

			main.fractalgenerator.getTemplate().setNrOfThreads(Convert.ToInt32(combo.SelectedValue));
		}

		private void color_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
		{
			var picker = sender as ColorPicker;
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			main.fractalgenerator.getTemplate().setColor(picker.Color);
		}

		private void x1_TextChanged(object sender, TextChangedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			TextBox t = (TextBox)sender;

			FractalTemplate.FractalTemplate f = main.fractalgenerator.getTemplate();
			try
			{
				f.x1 = Convert.ToDouble(t.Text);
			}catch(Exception)
			{

			}
		}

		private void x2_TextChanged(object sender, TextChangedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			TextBox t = (TextBox)sender;

			FractalTemplate.FractalTemplate f = main.fractalgenerator.getTemplate();
			try
			{
				f.x2 = Convert.ToDouble(t.Text);
			}
			catch (Exception)
			{

			}
		}

		private void y2_TextChanged(object sender, TextChangedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			TextBox t = (TextBox)sender;

			FractalTemplate.FractalTemplate f = main.fractalgenerator.getTemplate();
			try
			{
				f.y2 = Convert.ToDouble(t.Text);
			}
			catch (Exception)
			{

			}
		}

		private void y1_TextChanged(object sender, TextChangedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			TextBox t = (TextBox)sender;

			FractalTemplate.FractalTemplate f = main.fractalgenerator.getTemplate();
			try
			{
				f.y1 = Convert.ToDouble(t.Text);
			}
			catch (Exception)
			{

			}
		}
	}
}
