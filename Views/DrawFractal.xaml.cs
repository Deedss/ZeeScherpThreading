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
	public sealed partial class DrawFractal : Page
	{
		
		public DrawFractal()
		{
			this.InitializeComponent();

			

			generate();
		}

		private void generate()
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;
			main.fractalgenerator.setStackPanel(aids);

			log.Text = "";

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int threadDone = 0;

			main.fractalgenerator.generate((string x) => {
				//Callback when thread is done generating
				log.Text += "\nThread: " + x + " is done";
				threadDone++;
				if (threadDone == main.fractalgenerator.getTemplate().getNrOfThreads())
				{
					//When all threads done stop the stopwatch
					stopwatch.Stop();
					log.Text += "\n\nTotal time to draw: " + stopwatch.Elapsed.ToString();
				}
				return 1;
			});
		}

		public StackPanel getCanvas()
		{
			return this.aids;
		}

		private void aids_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;

			if (!main.fractalgenerator.isGenerating())
			{
				var delta = e.GetCurrentPoint((UIElement)sender).Properties.MouseWheelDelta;
				//Move up
				double step = Convert.ToDouble(delta) / 1000.0;

				FractalTemplate.FractalTemplate f = main.fractalgenerator.getTemplate();

				f.x1 += step;
				f.x2 -= step;
				f.y1 -= step;
				f.y2 += step;
				main.fractalgenerator.setTemplate(f);
				this.generate();
			}
		}

	
	}
}
