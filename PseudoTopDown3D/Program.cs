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
            int mapMode = 1;
            MapGenerator mapGenerator = new(1600, 1600, 0.008f);
            byte[,] shadowMap = mapGenerator.shadowMap;
            Color[,] colorMap = mapGenerator.GenerateColorMap();
            float[,] heightMap = mapGenerator.heightMap;
            Player player = new();

            Minimap minimap = new(heightMap, colorMap, shadowMap);
            Sprite heightMM = minimap.GetHeightMap();
            Sprite colorMM = minimap.GetColorMap();
            Sprite shadowMM = minimap.GetShadowMap();
            CircleShape shape = new(5, 3);
           


            heightMM.Scale = new(0.1f, 0.1f);
            colorMM.Scale = new(0.1f, 0.1f);
            shadowMM.Scale = new(0.1f, 0.1f);



            window.SetVerticalSyncEnabled(false);

            while (window.IsOpen)
            {

                window.Display();
                window.DispatchEvents();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                {
                    mapMode = 1;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                {
                    mapMode = 2;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                {
                    mapMode = 3;
                }

                window.Clear();

                player.Update();
                Renderer renderer = new(player, SWidth, SHeight, 2000, MathF.PI / 6, heightMap, colorMap);
                Sprite screen = renderer.RayCast();
                window.Draw(screen);
                switch (mapMode)
                {
                    case 1:
                        window.Draw(colorMM);
                        break;

                    case 2:
                        window.Draw(heightMM);
                        break;

                    case 3:
                        window.Draw(shadowMM);
                        break;
                }
                shape.Position = player.Position * 0.1f;
                window.Draw(shape);



            }
        }
    }
}