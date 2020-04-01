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

		/// <summary>
		/// Start generating the fractals defined in fractal generator.
		/// A callback will be made when generating of a thread is complete
		/// </summary>
		private void generate()
		{
			var frame = (Frame)Window.Current.Content;
			var main = (MainPage)frame.Content;
			main.fractalgenerator.setStackPanel(canvas);

			log.Text = "";

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int threadDone = 0;

			//Start generating, callback will be called when a thread is done working
			main.fractalgenerator.generate((string x) => {
				//Callback when thread is done generating
				log.Text += "\nThread: " + x + " is done";
				threadDone++;
				//Check if count of done threads equals amount of threads in generator
				if (threadDone == main.fractalgenerator.getTemplate().getNrOfThreads())
				{
					//When all threads done stop the stopwatch and display the final time
					stopwatch.Stop();
					log.Text += "\n\nTotal time to draw: " + stopwatch.Elapsed.ToString();
				}
				return 1;
			});
		}
	}
}
