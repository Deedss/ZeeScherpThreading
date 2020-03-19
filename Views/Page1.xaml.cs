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
	public sealed partial class Page1 : Page
	{
		FractalTemplate.MandelBrot fractal;
		FractalGenerator fg;
		double correctX;
			double correctY;
		public Page1()
		{
			this.InitializeComponent();
			fg = new FractalGenerator(aids);
			fractal = new FractalTemplate.MandelBrot();
			fractal.setWidth(100);
			fractal.setHeight(100);

			fg.generate(fractal, 4);
			
		}

	
		private void aids_PointerMoved(object sender, PointerRoutedEventArgs e)
		{
			Point p = e.GetCurrentPoint(sender as StackPanel).Position;
			StackPanel s = sender as StackPanel;

			/*correctX = (p.X * fractal.getWidth()) / s.ActualWidth;
			correctY = (p.Y * fractal.getHeight()) / s.ActualWidth;*/
			
		}

		private void aids_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
		{
			if (!fg.isGenerating())
			{
				var delta = e.GetCurrentPoint((UIElement)sender).Properties.MouseWheelDelta;
				//Move up
				double step = Convert.ToDouble(delta) / 1000.0;

				fractal.x1 += step;
				fractal.x2 -= step;
				fractal.y1 -= step;
				fractal.y2 += step;

				fg.generate(fractal, 4);
			}
		}
	}
}
