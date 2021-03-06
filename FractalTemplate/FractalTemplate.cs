﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZeeScherpThreading.FractalTemplate
{
    /// <summary>
    /// FractalTemplate base class
    /// NOTE: !!Items moeten public zijn in verband met omzetten naar JSON!!
    /// </summary>
    public abstract class FractalTemplate
    {
        public int width, height, iterations = 1, nrOfThreads;
        public double x1, x2, y1, y2;
        public String name;
        public Windows.UI.Color color;
      
        public void setColor(Windows.UI.Color c)
        {
            this.color = c;
        }
        public Windows.UI.Color getColor()
        {
            return this.color;
        }

        public void setNrOfThreads(int nr)
        {
            this.nrOfThreads = nr;
        }

        public int getNrOfThreads()
        {
            return this.nrOfThreads;
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
