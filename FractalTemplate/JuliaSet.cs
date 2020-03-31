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
            this.name = "Standaard JuliaSet";
        }

        public override int[,] calculate(FractalPart fa)
        {
            int w;
            int w_max = 255;
            int[,] pixels = new int[fa.getWidth(), fa.getHeight()];

            //real and imaginary part of the constant c, determinate shape of the Julia Set
            double cX, cY;
            double zx, zy;

            cX = -0.7;
            cY = -0.27015;

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
                        double tmp = zx * zx - zy * zy + cX;
                        zy = 2.0 * zx * zy + cY;
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
