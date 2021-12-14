using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PseudoTopDown3D
{
    internal class Player
    {
        Vector2f position;
        float angle;
        float height;
        double pitch;
        readonly float turningVel;
        readonly float Velocity;
        public Player()
        {
            Position = new Vector2f(0, 0);
            Angle = MathF.PI / 4;
            Height = 128;
            Pitch = 90;
            turningVel = 0.1f;
            Velocity = 10;
        }

        public void Update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                pitch += Velocity;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                pitch -= Velocity;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                angle -= turningVel;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                angle += turningVel;
            }




            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                height += Velocity;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                height -= Velocity;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                float x = Velocity * MathF.Cos(angle);
                float y = Velocity * MathF.Sin(angle);
                position += new Vector2f(x, y);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                float x = Velocity * MathF.Cos(angle);
                float y = Velocity * MathF.Sin(angle);
                position -= new Vector2f(x, y);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                float x = Velocity * MathF.Sin(angle);
                float y = -Velocity * MathF.Cos(angle);
                position += new Vector2f(x, y);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                float x = -Velocity * MathF.Sin(angle);
                float y = Velocity * MathF.Cos(angle);
                position += new Vector2f(x, y);
            }
        }
        public float Angle { get => angle; set => angle = value; }
        public Vector2f Position { get => position; set => position = value; }
        public float Height { get => height; set => height = value; }
        public double Pitch { get => pitch; set => pitch = value; }
    }
}
