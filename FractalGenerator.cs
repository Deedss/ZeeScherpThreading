using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ZeeScherpThreading
{
    /// <summary>
    /// FractalGenerator manages all fractal templates
    /// </summary>
    public class FractalGenerator
    {
        private List<FractalPart> fractalParts = new List<FractalPart>();

        private FractalTemplate.FractalTemplate fractal;
        private StackPanel sp;

        /// <summary>
        /// Set UI element to draw elements on
        /// </summary>
        public void setStackPanel(StackPanel sp)
        {
            this.sp = sp;
        }

        /// <summary>
        /// Checks if generator is still generating fractal parts
        /// </summary>
        public Boolean isGenerating()
        {
            foreach (FractalPart part in this.fractalParts)
            {
                if (part.imageArray == null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Set current template to draw fractal parts with
        /// </summary>
        public void setTemplate(FractalTemplate.FractalTemplate fractal)
        {
            this.fractal = fractal;
        }

        /// <summary>
        /// Return current fractal template
        /// </summary>
        public FractalTemplate.FractalTemplate getTemplate()
        {
            return this.fractal;
        }

        /// <summary>
        /// Clear all fractal parts from the UI
        /// </summary>
        public void clear()
        {
            this.sp.Children.Clear();
        }

        /// <summary>
        /// Generate fractal parts based on current template and update UI
        /// A fractal gets split up into x amount of parts
        /// Every fractalpart gets added to a new thread, when the thread is done a callback method will be called and the UI will be updated.
        /// </summary>
        public void generate(Action<FractalPart> callback)
        {
            this.fractalParts = new List<FractalPart>();

            //For amount of threads split the fractal up into FractalParts
            double step = Convert.ToDouble((Math.Abs(fractal.y1) + Math.Abs(fractal.y2))) / Convert.ToDouble(this.fractal.getNrOfThreads());
            double y1 = fractal.y1;
            double y2 = (y1 - step);

            this.sp.Children.Clear();

            //Add placeholder images at right locations
            int partNum = 0;
            while (partNum != this.fractal.getNrOfThreads())
            {
                Windows.UI.Xaml.Controls.Image img = new Windows.UI.Xaml.Controls.Image();
                this.sp.Children.Add(img);
                partNum++;
            }

            //Generate the threads for all parts of the fractal
            //NOTE: Generating the parts and threads are done inside another thread to prevent UI lock on slower computers
  
            new Thread(() =>
            {
                int pos = 0;
                while (pos != this.fractal.getNrOfThreads())
                {
                    FractalPart part = new FractalPart(fractal.x1,
                        fractal.x2, y1, y2,
                        fractal.getWidth(), fractal.getHeight() / this.fractal.getNrOfThreads(), pos);
                    this.fractalParts.Add(part);
                    pos++;

                    y1 -= step;
                    y2 = (y1 - step);
                   
                    //Calculate every fractalpart on a new thread.
                    Thread thr = new Thread(() => generateFractalPart(part, fractal, callback));
                    thr.Start();
                }
            }).Start();
        }

        /// <summary>
        /// UI Thrad callback when generating fractal part is done
        /// Part will be converted to bitmap and added to UI
        /// </summary>
        public async Task addFractalPartToUIAsync(FractalPart part)
        {
           Windows.UI.Xaml.Controls.Image img = sp.Children[part.getPos()] as Windows.UI.Xaml.Controls.Image;
           WriteableBitmap w = new WriteableBitmap(part.getWidth(), part.getHeight());
            
           using (Stream stream = w.PixelBuffer.AsStream()) { await stream.WriteAsync(part.imageArray, 0, part.imageArray.Length); }
           img.Source = w;
        }

        /// <summary>
        /// Calculate and generate the fractalpart pixels and store in pixels array
        /// </summary>
        private async void generateFractalPart(FractalPart part, FractalTemplate.FractalTemplate fractal, Action<FractalPart> callback)
        {
            part.imageArray = new byte[part.getWidth() * part.getHeight() * 4];
            //Call the calculat fractal method
            int[,] pixels = fractal.calculate(part);
            //Generate bitmap from pixeldata
            int x = 0;
            int y = 0;
            for (int i = 0; i < part.imageArray.Length; i += 4)
            {
                //BGRA format
                int R = 0, G = 0, B = 0;

                if (fractal.getColor().R != 0 && fractal.getColor().G != 0 && fractal.getColor().B != 0)
                {
                    R = pixels[x, y] % this.fractal.getColor().R;
                    G = pixels[x, y] % this.fractal.getColor().G;
                    B = pixels[x, y] % this.fractal.getColor().B;
                }
                else
                {
                    R = pixels[x, y];
                    G = pixels[x, y];
                    B = pixels[x, y];
                }

                part.imageArray[i] = Convert.ToByte(B); //blue
                part.imageArray[i + 1] = Convert.ToByte(G); //green
                part.imageArray[i + 2] = Convert.ToByte(R); //red
                part.imageArray[i + 3] = 255; // Alpha
                x++;
                if (x == part.getWidth())
                {
                    y++;
                    x = 0;
                }
            }

            //Callback to UI thread when generating is done, add to list and refresh current fractal parts on screen
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,  () =>
            {
                //callback to notify we are done
                callback(part);
            });

        }

    }
}
