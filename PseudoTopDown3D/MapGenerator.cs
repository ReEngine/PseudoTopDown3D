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
        private readonly float frequency;



        public uint Width { get => width; }
        public uint Height { get => height; }


        public MapGenerator(uint x, uint y, float frequency)
        {
            this.width = x;
            this.height = y;
            this.frequency = frequency;
            Noise.GenerateNoiseMap(width, height, frequency);
        }


        public Texture GenerateMap()
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(width, height, frequency);
            byte[] result = new byte[width * height * 4];
            for (uint x = 0; x < width; x++)
            {
                for (uint y = 0; y < height; y++)
                {
                    uint i = 4 * (x + (Width * y));
                    result[i] = (byte)(noiseMap[x, y] * 255);
                    result[i + 1] = (byte)(noiseMap[x, y] * 255);
                    result[i + 2] = (byte)(noiseMap[x, y] * 255);
                    result[i + 3] = (byte)(255);
                }
            }
            Texture texture = new(width, height);
            texture.Update(result);


            return texture;
        }

    }
}
