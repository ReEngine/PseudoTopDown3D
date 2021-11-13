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
        static readonly byte maxHeight = 255;
        static readonly Texture MainViewPort = new(Width, Height);

        static readonly Color[,,] layers = new Color[Width, Height, maxHeight];
        static readonly RenderWindow window = new(new VideoMode(SWidth, SHeight), "Demo", Styles.Default);

        public static void Main()
        {
            FastNoiseLite noise = new(new Random().Next());
            //FastNoiseLite noise = new(1244);

            noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            noise.SetFrequency(0.001f);
            noise.SetFractalType(FastNoiseLite.FractalType.FBm);
            noise.SetFractalOctaves(5);
            noise.SetFractalLacunarity(2.8f);
            noise.SetFractalGain(0.5f);
            noise.SetFractalWeightedStrength(-0.1f);


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
                        float noiseVal = noise.GetNoise(x + xOffset, y - yOffset, zOffset);
                        noiseVal += 1;
                        noiseVal /= 2;
                        byte val = (byte)(noiseVal * maxHeight);

                        for (byte l = 0; l < maxHeight; l++)
                        {
                            byte lByte = (byte)(l * 2);
                            if (l <= val)
                            {
                                if (l < 128) //Water
                                {
                                    layers[x, y, l] = new(0, 0, 255, 1);
                                }
                                if (l > 128)//Sand
                                {
                                    
                                    layers[x, y, l] = new Color(227, 227, 50, 255);
                                }
                                if (l > 130)//Grass
                                {
                                    layers[x, y, l] = new Color(0, 50, 0, 255);
                                }
                                if (l > 130)//LightGrass
                                {
                                    layers[x, y, l] = new Color(0, 100, 0, 255);
                                }
                                if (l > 140)//DarkGrass
                                {
                                    layers[x, y, l] = new Color(0, 50, 0, 255);
                                }
                                if (l > 145)//LightRock
                                {
                                    layers[x, y, l] = new Color(79, 57, 19, 255);
                                }
                                if (l > 150)//DarkRock
                                {
                                    layers[x, y, l] = new Color(59, 42, 13, 255);
                                }
                                if (l > 155)//Snow
                                {
                                    layers[x, y, l] = new Color(255, 255, 255, 255);
                                }
                                if (l > 158)//Sky
                                {
                                    layers[x, y, l] = new Color(0, 0, 0, 0);
                                }

                            }
                            else
                            {
                                layers[x, y, l] = Color.Transparent;
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
            for (int l = 0; l < maxHeight; l++)
            {
                for (uint x = 0; x < Width; x++)
                {
                    for (uint y = 0; y < Height; y++)
                    {
                        uint i = 4 * (x + (Width * y));
                        cpixels[x + (Width * y)] = layers[x, y, l];
                        pixels[i + 0] = cpixels[i / 4].R;
                        pixels[i + 1] = cpixels[i / 4].G;
                        pixels[i + 2] = cpixels[i / 4].B;
                        pixels[i + 3] = cpixels[i / 4].A;
                    }
                }
                Texture layerTexture = new(Width, Height);
                layerTexture.Update(pixels);
                Sprite layerSprite = new(layerTexture);
                double multiplyer = 0.5;
                float scalator = (l / (float)maxHeight * (float)multiplyer + 1);
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