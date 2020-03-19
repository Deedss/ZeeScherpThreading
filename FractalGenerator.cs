﻿using System;
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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ZeeScherpThreading
{
    class FractalGenerator
    {
        private List<FractalPart> fractalParts = new List<FractalPart>();
        private int nrOfThreads;

        private StackPanel sp;

        public FractalGenerator(StackPanel sp)
        {
            this.sp = sp;
        }

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

        public void generate(FractalTemplate.FractalTemplate fractal, int nrOfThreads)
        {
            this.nrOfThreads = nrOfThreads;
            this.fractalParts = new List<FractalPart>();

            //For amount of threads split the fractal up into FractalParts
            double step = Convert.ToDouble((Math.Abs(fractal.y1) + Math.Abs(fractal.y2))) / Convert.ToDouble(this.nrOfThreads);
            double y1 = fractal.y1;
            double y2 = (y1 - step);

            int partNum = 0;
            while (partNum != this.nrOfThreads)
            {
                this.fractalParts.Add(new FractalPart(fractal.x1,
                    fractal.x2,y1,y2,
                    fractal.getWidth(), fractal.getHeight() / this.nrOfThreads, partNum));
                partNum++;

                y1 -= step;
                y2 = (y1 - step);

            }

            //Calculate every fractalpart on a new thread.
            foreach (FractalPart part in this.fractalParts)
            {
                //Add placeholder part to show loading
                this.sp.Children.Add(new Windows.UI.Xaml.Controls.Image());
                Thread thr = new Thread(() => generateFractalPart(part,fractal));
                
                thr.Start();
            }
        }

        //Callback from thread when generating of fractal is done
        private void addFractalPartToUI(FractalPart part)
        {
            WriteableBitmap fractalBitmap = new WriteableBitmap(part.getWidth(), part.getHeight());
            using (Stream stream = fractalBitmap.PixelBuffer.AsStream()) { stream.Write(part.imageArray, 0, part.imageArray.Length); }
            //When generate of part is done re-draw ALL parts on canvas again
             Windows.UI.Xaml.Controls.Image img = new Windows.UI.Xaml.Controls.Image();
             img.Source = fractalBitmap;
            //img.Stretch = Windows.UI.Xaml.Media.Stretch.None;
            //img.Margin = new Thickness(0, 1, 0, 0);
            sp.Children.RemoveAt(part.pos);
            sp.Children.Insert(part.pos, img);
            }
        
        private async void generateFractalPart(FractalPart part, FractalTemplate.FractalTemplate fractal)
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

                R = pixels[x, y];
                G = pixels[x, y];
                B = pixels[x, y];

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

            //System.Threading.Thread.Sleep(new Random().Next(3,10) * 1000);

            //Callback to UI thread when generating is done, add to list and refresh current fractal parts on screen
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            { 
                this.addFractalPartToUI(part);
            });

        }

    }
}
