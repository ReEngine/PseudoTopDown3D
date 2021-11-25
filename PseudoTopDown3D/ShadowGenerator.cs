using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PseudoTopDown3D
{
    internal class ShadowGenerator
    {
        static public byte[,] GenerateShadowMap(float[,] noiseMap, Vector2f lightDirection)
        {
            float lightDirectionLength = MathF.Sqrt(lightDirection.X * lightDirection.X + lightDirection.Y * lightDirection.Y);
            Vector2f Light = new(lightDirection.X / lightDirectionLength, lightDirection.Y / lightDirectionLength);
            byte[,] result = new byte[noiseMap.GetLength(0), noiseMap.GetLength(1)];
            for (int x = 0; x < noiseMap.GetLength(0); x++)
            {
                for (int y = 0; y < noiseMap.GetLength(1); y++)
                {
                    float reference = noiseMap[x, y];
                    float sX = x;
                    float sY = y;
                    int shadowLength = 0;
                    while ((sX >= 0 & sX < noiseMap.GetLength(0)) & (sY >= 0 & sY < noiseMap.GetLength(1)) & shadowLength < reference)
                    {
                        if (noiseMap[(int)sX, (int)sY] < reference)
                        {
                            result[(int)sX, (int)sY] = 100;
                            shadowLength++; 
                        }
                        sX -= Light.X;
                        sY -= Light.Y;
                    }
                }
            }
            return result;
        }
    }
}
