using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoTopDown3D
{
    public static class Noise
    {
        public static float[,] GenerateNoiseMap(uint mapWidth, uint mapHeight, float scale)
        {
            FastNoiseLite noise = new(new Random().Next());
            noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            noise.SetFrequency(scale);
            noise.SetFractalType(FastNoiseLite.FractalType.None);
            float[,] noiseMap = new float[mapWidth, mapHeight];



            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    float perlinValue = (noise.GetNoise(x, y)+1)/2;
                    noiseMap[x, y] = perlinValue;
                }
            }

            return noiseMap;
        }
    }
}
