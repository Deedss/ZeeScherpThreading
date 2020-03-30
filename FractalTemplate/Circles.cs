using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeeScherpThreading.FractalTemplate
{
    class Circles : MandelBrot
    {
        public Circles()
        {
            this.x1 = -0.86697;
            this.x2 = -0.58882;
            this.y1 = 0.152347;
            this.y2 = 0.421013;
            this.name = "Circles";
        }

        
    }
}
