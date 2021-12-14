using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PseudoTopDown3D
{
    internal class Renderer
    {
        readonly uint screenWidth;
        readonly uint screenHeight;
        readonly int mapHeight;
        readonly int mapWidth;
        readonly float fov;
        readonly uint rayDistance;
        readonly Player player;
        readonly float[,] heightMap;
        readonly Color[,] colorMap;
        public float[,] FOM = FalloffGenerator.GenerateFallOffMap(900);
        public Renderer(Player player, uint ScreenWidth, uint ScreenHeight, uint RayDistance, float FOV, float[,] HeightMap, Color[,] ColorMap)
        {
            this.screenWidth = ScreenWidth;
            this.screenHeight = ScreenHeight;
            this.fov = FOV;
            this.rayDistance = RayDistance;
            this.heightMap = HeightMap;
            this.mapWidth = heightMap.GetLength(0);
            this.mapHeight = heightMap.GetLength(1);
            this.colorMap = ColorMap;
            this.player = player;

        }

        public Sprite RayCast()
        {
            float[] yBuffer = new float[this.screenWidth];
            float rayAngle = player.Angle - this.fov / 2;
            Color[,] screen = new Color[this.screenWidth, this.screenHeight];

            for (int numRay = 0; numRay < screenWidth; numRay++)
            {
                bool firstContact = false;
                float sinA = MathF.Sin(rayAngle);
                float cosA = MathF.Cos(rayAngle);

                for (int depth = 1; depth < rayDistance; depth++)
                {
                    int x = (int)(player.Position.X + depth * cosA);
                    if (0 < x & x < mapWidth)
                    {
                        int y = ((int)(player.Position.Y + depth * sinA));
                        if (0 < y & y < mapHeight)
                        {
                            //depth *= (int)MathF.Cos(player.Angle - rayAngle);
                            int heightOnScreen = (int)((player.Height - this.heightMap[x, y]) / depth * 920 + player.Pitch);

                            if (!firstContact)
                            {
                                yBuffer[numRay] = MathF.Min(heightOnScreen, screenHeight);
                                firstContact = true;
                            }

                            if (heightOnScreen < 0)
                            {
                                heightOnScreen = 0;
                            }

                            if (heightOnScreen < yBuffer[numRay])
                            {
                                for (int screenY = heightOnScreen; screenY < yBuffer[numRay]; screenY++)
                                {
                                    screen[numRay, screenY] = this.colorMap[x, y];

                                }
                                yBuffer[numRay] = heightOnScreen;
                            }
                        }
                    }
                }
                rayAngle += fov / screenWidth;
            }
            Texture screenTexture = new(this.screenWidth, this.screenHeight);
            Color[] cpixels = new Color[screenWidth * screenHeight * 4];
            byte[] pixels = new byte[screenWidth * screenHeight * 4];
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    uint i = (uint)(4 * (x + (screenWidth * y)));
                    cpixels[x + (screenWidth * y)] = screen[x, y];
                    pixels[i + 0] = cpixels[i / 4].R;
                    pixels[i + 1] = cpixels[i / 4].G;
                    pixels[i + 2] = cpixels[i / 4].B;
                    pixels[i + 3] = cpixels[i / 4].A;
                }
            }
            screenTexture.Update(pixels);

            return new Sprite(screenTexture);
        }
    }
}
