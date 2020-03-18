using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ZeeScherpThreading
{
    class FractalGenerator
    {
        private WriteableBitmap fractalImage;
        private int[,] pixels;
        private List<FractalPart> fractalParts;
        private int nrOfThreads;

        public FractalGenerator()
        {

        }

        public void generate(FractalTemplate.FractalTemplate fractal, int nrOfThreads, int iterations)
        {

        }

        public WriteableBitmap getBitmap()
        {
            return this.fractalImage;
        }
    }
}
