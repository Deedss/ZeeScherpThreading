using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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
		private Frame frame;
		private MainPage main;

		private Stopwatch stopwatch;
		public DrawFractal()
		{
			this.InitializeComponent();
			frame = (Frame)Window.Current.Content;
			main = (MainPage)frame.Content;

			log.Text = "";

			generate();
		}

		/// <summary>
		/// Start generating the fractals defined in fractal generator.
		/// A callback will be made when generating of a thread is complete
		/// </summary>
		private void generate()
		{
			main.fractalgenerator.setStackPanel(canvas);

			stopwatch = new Stopwatch();
			stopwatch.Start();

			//Start generating, callback will be called when a single thread is done working
			main.fractalgenerator.generate((FractalPart part) => this.generateCallback(part));
		}

		//Number of threads that are done
		int threadDone = 0;
		//Callback to UI thread
		private async void generateCallback(FractalPart part)
		{
			//Add generated fractal part to the UI
			await main.fractalgenerator.addFractalPartToUIAsync(part);
		
			//Add log entry with thread number
			log.Text += "\nThread: " + part.getPos() + " is done";
			threadDone++;
			//Check if count of done threads equals amount of threads in generator
			if (threadDone == main.fractalgenerator.getTemplate().getNrOfThreads())
			{
				//When all threads done stop the stopwatch and display the final time
				stopwatch.Stop();
				log.Text += "\n\nTotal time to draw: " + stopwatch.Elapsed.ToString();
			}
		}
	}
}
