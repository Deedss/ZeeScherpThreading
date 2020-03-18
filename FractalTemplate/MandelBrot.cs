using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeeScherpThreading.FractalTemplate
{
    class MandelBrot : FractalTemplate
    {
        public MandelBrot()
        {
            this.x1 = -2.0;
            this.x2 = 1.0;
            this.y1 = 1.0;
            this.y2 = -1.0;
        }

        public override int[,] calculate(FractalPart fa)
        {
            int w;
            int w_max = 255;
            int[,] pixels = new int[fa.getWidth(), fa.getHeight()];

            double dx = (fa.x2 - fa.x1) / fa.getWidth();
            double dy = (fa.y2 - fa.y1) / fa.getHeight();

            double cr, ci, zr, zi, zr2, zi2, zrt;

            // For every pixel width x height of this FractalPart
            for (int Xcount = 0; Xcount < fa.getWidth(); Xcount++)
            {
                for (int Ycount = 0; Ycount < fa.getHeight(); Ycount++)
                {
                    cr = fa.x1 + dx * Xcount;
                    ci = fa.y1 + dy * Ycount;

                    w = 0;
                    zr = 0;
                    zi = 0;
                    zr2 = 0;
                    zi2 = 0;

                    // Fractal berekenen
                    while (((zr2 + zi2) < 4) && (w < w_max))
                    {
                        zrt = zr2 - zi2 + cr;
                        zi = 2 * zr * zi + ci;
                        zr = zrt;
                        zr2 = Math.Pow(zr, 2);
                        zi2 = Math.Pow(zi, 2);
                        w++;
                    }
                    pixels[Xcount, Ycount] = (w < w_max) ? w : w_max;
                }
            }
            return pixels;
        }
    }
}
