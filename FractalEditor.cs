using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ZeeScherpThreading
{
    class FractalEditor
    {
        private List<FractalTemplate.FractalTemplate> fractals;

        public FractalEditor()
        {

        }

        public void create(FractalTemplate.FractalTemplate fractal, Complex a, Complex b)
        {

        }

        public List<FractalTemplate.FractalTemplate> getFractals()
        {
            return this.fractals;
        }

        public void saveFractals(String filename)
        {
            //TODO JSON 
        }

        public void loadFractals(String filename)
        {
            //TODO JSON
        }
    }
}
