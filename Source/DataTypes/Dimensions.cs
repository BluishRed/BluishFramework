using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluishFramework
{
    public struct Dimensions
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Dimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}