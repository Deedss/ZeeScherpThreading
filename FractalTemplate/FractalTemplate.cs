using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZeeScherpThreading.FractalTemplate
{
    abstract class FractalTemplate
    {
        private int width, height, iterations = 1;
        public double x1, x2, y1, y2;

        public FractalTemplate()
        {

        }

        public int getWidth()
        {
            return this.width;
        }

        public void setWidth(int w)
        {
            this.width = w;
        }

        public int getHeight()
        {
            return this.height;
        }

        public void setHeight(int h)
        {
            this.height = h;
        }

        public int getIterations()
        {
            return this.iterations;
        }

        public void setIterations(int i)
        {
            this.iterations = i;
        }

        public abstract int[,] calculate(FractalPart fa);
    }
}
