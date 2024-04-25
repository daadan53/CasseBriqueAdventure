using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace CasseBrique
{
    internal class Player : Sprite
    {
        public Vector2 Velocity { get; set; }
        public bool onTheBric { get; set; }
        public bool onTheSide;
        public bool canJump;
        public Rectangle nextPositionX { get; set; }
        public Rectangle nextPositionY { get; set; }

        public Player(Texture2D pTexture, Rectangle pScreen) : base(pTexture, pScreen)
        {
            Velocity = Vector2.Zero;
            onTheBric = false;
            onTheSide = false;
        }

        // TEster les collisions de côtés 
        public void Move()
        { 

            if(!onTheSide)
            
            {
                Position += Speed;
            }

        }

        public override void Update()
        {
            if (Position.X < 0) 
            {
                Position = new Vector2(0, Position.Y);
            }
            if (Position.X + Width > Screen.Width)
            {
                Position = new Vector2(Screen.Width - Width, Position.Y);    
            }

            Position += Velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Speed = new Vector2(1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Speed = new Vector2(-1, 0);
                
            }
            else { Speed = new Vector2(0, 0); }

            // Jump
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && onTheBric)
            {
                Velocity = new Vector2(0, -4);
                onTheBric = false;
            }

            if (!onTheBric)
            {
                Velocity += new Vector2(0, 0.2f);
            }


            base.Update();
        }
    }
}
