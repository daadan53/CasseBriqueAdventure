using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    public class Ball : Sprite
    {
        public Ball(Texture2D pTexture, Rectangle pScreen) : base(pTexture, pScreen)
        {

        }

        public override void Update()
        {
            Position += Speed;
            if (Position.X > Screen.Width - Width) 
            { 
                SetPosition(Screen.Width - Width, Position.Y);
                BackSpeedX();
            }
            if (Position.X < 0) 
            {
                SetPosition(0, Position.Y);
                BackSpeedX();
            }
            if (Position.Y < 0)
            { 
                SetPosition(Position.X, 0);
                BackSpeedY();
            }

            base.Update();
        }
    }
}
