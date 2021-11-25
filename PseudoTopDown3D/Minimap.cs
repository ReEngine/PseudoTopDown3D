using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PseudoTopDown3D
{
    internal class Minimap
    {
        readonly float[,] heightMap;
        readonly Color[,] colorMap;
        readonly byte[,] shadowMap;
        public Minimap(float[,] HeightMap, Color[,] ColorMap, byte[,] ShadowMap)
        {
            heightMap = HeightMap;
            colorMap = ColorMap;
            shadowMap = ShadowMap;
        }

        public Sprite GetHeightMap()
        {
            byte[] colors = new byte[this.heightMap.GetLength(0) * this.heightMap.GetLength(1) * 4];
            for (int x = 0; x < this.heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.heightMap.GetLength(1); y++)
                {
                    uint i = (uint)(4 * (x + (heightMap.GetLength(0) * y)));
                    colors[i + 0] = (byte)heightMap[x, y];
                    colors[i + 1] = (byte)heightMap[x, y];
                    colors[i + 2] = (byte)heightMap[x, y];
                    colors[i + 3] = 255;
                }
            }
            Texture texture = new((uint)heightMap.GetLength(0), (uint)heightMap.GetLength(1));
            texture.Update(colors);
            return new Sprite(texture);

        }

        public Sprite GetShadowMap()
        {
            byte[] colors = new byte[this.shadowMap.GetLength(0) * this.shadowMap.GetLength(1) * 4];
            for (int x = 0; x < this.shadowMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.shadowMap.GetLength(1); y++)
                {
                    uint i = (uint)(4 * (x + (shadowMap.GetLength(0) * y)));
                    colors[i + 0] = (byte)shadowMap[x, y];
                    colors[i + 1] = (byte)shadowMap[x, y];
                    colors[i + 2] = (byte)shadowMap[x, y];
                    colors[i + 3] = 255;
                }
            }
            Texture texture = new((uint)shadowMap.GetLength(0), (uint)shadowMap.GetLength(1));
            texture.Update(colors);
            return new Sprite(texture);

        }
        public Sprite GetColorMap()
        {
            byte[] colors = new byte[this.colorMap.GetLength(0) * this.colorMap.GetLength(1) * 4];
            for (int x = 0; x < this.colorMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.colorMap.GetLength(1); y++)
                {
                    uint i = (uint)(4 * (x + (colorMap.GetLength(0) * y)));
                    colors[i + 0] = colorMap[x, y].R;
                    colors[i + 1] = colorMap[x, y].G;
                    colors[i + 2] = colorMap[x, y].B;
                    colors[i + 3] = 255;
                }
            }
            Texture texture = new((uint)colorMap.GetLength(0), (uint)colorMap.GetLength(1));
            texture.Update(colors);
            return new Sprite(texture);

        }
    }
}
