using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PseudoTopDown3D
{
    public static class Program
    {
        static readonly uint SWidth = 1600;
        static readonly uint SHeight = 900;

        static readonly float scale = 15;

        static readonly uint Width = (uint)(SWidth / scale);
        static readonly uint Height = (uint)(SHeight / scale);

        static readonly byte[] pixels = new byte[Width * Height * 4];
        static readonly Color[] cpixels = new Color[Width * Height];

        static int xOffset = 0;
        static int yOffset = 0;
        static float zOffset = 0;
        static readonly byte maxHeight = 128;
        static readonly Texture MainViewPort = new(Width, Height);

        static readonly List<Color[,]> layers = new();
        static readonly RenderWindow window = new(new VideoMode(SWidth, SHeight), "Demo", Styles.Default);

        public static void Main()
        {
            for (byte l = 0; l < maxHeight; l++)
            {
                layers.Add(new Color[Width, Height]);
            }
            FastNoiseLite noise = new(124489);
            noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            noise.SetFrequency(0.01f);
            noise.SetFractalType(FastNoiseLite.FractalType.None);
            noise.SetFractalOctaves(2);
            noise.SetFractalLacunarity(1);
            window.SetVerticalSyncEnabled(false);


            while (window.IsOpen)
            {
                //window.Clear(Color.White);
                window.Display();
                window.DispatchEvents();
                Sprite mainViewPort = new(MainViewPort);
                mainViewPort.Scale = new Vector2f(scale, scale);
                //window.Draw(mainViewPort);
                //window.Display();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                {
                    yOffset++;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                {
                    xOffset--;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    yOffset--;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                {
                    xOffset++;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    zOffset += 0.1f;
                }



                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        byte val = (byte)((byte)(noise.GetNoise(x + xOffset, y - yOffset, zOffset) * maxHeight));

                        for (int l = 0; l < maxHeight; l++)
                        {
                            if (l == val)
                            {
                                layers[l][x, y] = new Color((byte)(l), (byte)(l), 0, 255);
                            }
                            else
                            {
                                layers[l][x, y] = new(0,0,255,0);
                            }
                            
                        }
                    }
                }
                window.Clear();
                Update();




            }

        }
        static void Update()
        {
            //for (uint x = 0; x < pixels.Length; x++)
            //{
            //    pixels[x] = 0;

            //}
            for (int l = 0; l < layers.Count; l++)
            {
                for (uint x = 0; x < Width; x++)
                {
                    for (uint y = 0; y < Height; y++)
                    {
                        uint i = 4 * (x + (Width * y));
                        cpixels[x + (Width * y)] = layers[l][x, y];
                        pixels[i + 0] = cpixels[i / 4].R;
                        pixels[i + 1] = cpixels[i / 4].G;
                        pixels[i + 2] = cpixels[i / 4].B;
                        pixels[i + 3] = cpixels[i / 4].A;
                    }
                }
                Texture layerTexture = new(Width, Height);
                layerTexture.Update(pixels);
                Sprite layerSprite = new(layerTexture);
                double multiplyer = 1;
                float scalator = ((float)l / (float)maxHeight * (float)multiplyer + (float)1);
                layerSprite.Scale = new(scale * scalator, scale * scalator);
                float posOffsetX = scale * (scalator - 1) / 2 * -Width;
                float posOffsetY = scale * (scalator - 1) / 2 * -Height;

                layerSprite.Position = new(posOffsetX, posOffsetY);
                window.Draw(new Sprite(layerSprite));
            }

            //MainViewPort.Update(pixels);
        }
    }
}