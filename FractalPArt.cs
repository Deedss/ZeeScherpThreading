using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ZeeScherpThreading
{
    /// <summary>
    /// A part of the fractal template
    /// A fractaltemplate can be split up into an x number of parts
    /// These parts will be calculated on a different thread and combined in the UI
    /// </summary>
    public class FractalPart
    {
        public double x1, x2, y1, y2;
        private int width, height, pos;
        public byte[] imageArray;

        public FractalPart() { }

        public FractalPart(double x1, double x2, double y1, double y2, int width, int height, int pos)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.width = width;
            this.height = height;
            this.pos = pos;
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

        public int getPos()
        {
            return this.pos;
        }

        public void setPos(int p)
        {
            this.pos = p;
        }

    }
}
