using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace PseudoTopDown3D
{
    internal class MapGenerator
    {
        private readonly uint width;
        private readonly uint height;
        public float[,] heightMap;


        public uint Width { get => width; }
        public uint Height { get => height; }


        public MapGenerator(uint x, uint y, float frequency)
        {
            this.width = x;
            this.height = y;
            this.heightMap = Noise.GenerateNoiseMap(width, height, frequency);
        }


        public Color[,] GenerateColorMap()
        {
             Color[,] result = new Color[width, height];
            for (uint x = 0; x < width; x++)
            {
                for (uint y = 0; y < height; y++)
                {
                    byte value = (byte)(this.heightMap[x, y] );
                    Color color;
                    if (value < 10)
                    {
                        color = new Color(0, 0, 255);
                    }
                    else if (value < 20)
                    {
                        color = new Color(255, 229, 50);
                    }
                    else if (value < 30)
                    {
                        color = new Color(76, 177, 12);
                    }
                    else if (value < 40)
                    {
                        color = new Color(46, 127, 10);
                    }
                    else if (value < 50)
                    {
                        color = new Color(28, 103, 11);
                    }
                    else if (value < 60)
                    {
                        color = new Color(103, 64, 17);
                    }
                    else if (value < 74)
                    {
                        color = new Color(74, 56, 33);
                    }
                    else if (value < 82)
                    {
                        color = new Color(85, 70, 51);
                    }
                    else if (value < 90)
                    {
                        color = new Color(89, 89, 89);
                    }
                    else if (value < 95)
                    {
                        color = new Color(113, 113, 113);
                    }
                    else
                    {
                        color = new Color(255, 255, 255);
                    }


                    result[x, y] = color;
                }
            }
            


            return result;
        }

    }
}
