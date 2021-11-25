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
            noise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
            noise.SetFrequency(scale);

            noise.SetFractalType(FastNoiseLite.FractalType.FBm);
            noise.SetFractalOctaves(5);
            noise.SetFractalLacunarity(2.2f);
            noise.SetFractalGain(0.3f);
            noise.SetFractalWeightedStrength(3f);

            noise.SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction.Euclidean);
            noise.SetCellularReturnType(FastNoiseLite.CellularReturnType.Distance2Mul);
            noise.SetCellularJitter(1);


            float[,] FOM = FalloffGenerator.GenerateFallOffMap((int)MathF.Max(mapWidth,mapHeight));
            float[,] noiseMap = new float[mapWidth, mapHeight];

            float min = 1000;
            float max = -1000;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    float perlinValue = (noise.GetNoise(x, y));
                    if (perlinValue <= min)
                    {
                        min = perlinValue;
                    }
                    if (perlinValue >= max)
                    {
                        max = perlinValue;
                    }
                    noiseMap[x, y] = (1 - FOM[x, y]) * 255 * Math.Clamp(Map(perlinValue, -0.5f, -0.19f, -1, 0) + 1, 0, 1);
                }
            }
            Console.WriteLine("Max = " + max + "\nMin = " + min);
            return noiseMap;
        }
        static float Map(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

    }
}
