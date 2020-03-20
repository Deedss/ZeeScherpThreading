using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZeeScherpThreading.FractalTemplate
{
    class JuliaSet : FractalTemplate
    {
        public JuliaSet()
        {
            this.x1 = -1.0;
            this.x2 = 1.0;
            this.y1 = -1.0;
            this.y2 = 1.0;
        }

        public override int[,] calculate(FractalPart fa)
        {
            int w;
            int w_max = 255;
            int[,] pixels = new int[fa.getWidth(), fa.getHeight()];

            //real and imaginary part of the constant c, determinate shape of the Julia Set
            double cX, cY;
            double moveX = 0.0, moveY = this.y1 / 2;
            double zx, zy;

            double dx = (fa.x2 - fa.x1) / fa.getWidth();
            double dy = (fa.y2 - fa.y1) / fa.getHeight();

            // works singlethreaded, has to be constant
            cX = -0.7;
            cY = -0.27015;

            Complex c = new Complex(0.7, -0.27015);
            //cX = (fa.x2 - fa.x1) / dx;
            //cY = (fa.y2 - fa.y1) / dy;

            // Loop through each 
            for (int Xcount = 0; Xcount < fa.getWidth(); Xcount++)
            {
                for (int Ycount = 0; Ycount < fa.getHeight(); Ycount++)
                {
                    w = 0;

                    zx = (fa.x2 - fa.x1) * (Xcount - fa.getWidth() / 2) / (0.5 * fa.getWidth());
                    zy = (fa.y2 - fa.y1) * (Ycount - fa.getHeight() / 2) / (0.5 * fa.getHeight());

                    while (zx * zx + (zy * zy) < 4 && w < w_max)
                    {
                        double tmp = zx * zx - zy * zy + c.Imaginary;
                        zy = 2.0 * zx * zy + c.Real;
                        zx = tmp;
                        w++;
                    }
                    pixels[Xcount, Ycount] = (w < w_max) ? w : w_max;
                }
            }

            return pixels;
        }
    }
}
