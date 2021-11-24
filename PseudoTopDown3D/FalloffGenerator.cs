using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoTopDown3D
{
    internal class FalloffGenerator
    {
        public static float[,] GenerateFallOffMap(int size)
        {
            float[,] map = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++)
                {
                    float x = i / (float)size * 2 - 1;
                    float y = k / (float)size * 2 - 1;

                    float value = MathF.Max(MathF.Abs(x), MathF.Abs(y));
                    map[i, k] = Evaluate(value);
                }
            }
            return map;
        }

        static float Evaluate(float value)
        {
            float a = 3;
            float b = 2.2f;

            return MathF.Pow(value, a) / (MathF.Pow(value, a) + MathF.Pow(b - b * value, a));
        }

    }
}
