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

        static readonly RenderWindow window = new(new VideoMode(SWidth, SHeight), "Demo", Styles.Default);

        public static void Main()
        {
            MapGenerator mapGenerator = new(900, 900, 0.008f);
            Color[,] colorMap = mapGenerator.GenerateColorMap();
            float[,] heightMap = mapGenerator.heightMap;
            Player player = new();

            window.SetVerticalSyncEnabled(false);

            while (window.IsOpen)
            {

                window.Display();
                window.DispatchEvents();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }

                window.Clear();

                player.Update();
                Renderer renderer = new(player, SWidth, SHeight, 2000, MathF.PI / 6, heightMap, colorMap);
                Sprite screen = renderer.RayCast();
                window.Draw(screen);

            }
        }
    }
}