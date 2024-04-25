using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CasseBrique
{
    public class Pad : Sprite
    {
        Random Rdm;
        Timer timer;
        float randomValueTarget;
        float randomValueCurrent;
        Ball ball;

        public Pad(Texture2D pTexture, Rectangle pScreen, Ball pBall) : base(pTexture, pScreen)
        {
            Rdm = new Random();
            ball = pBall;
            

            timer = new Timer(1000); // 1000 millisecondes = 1 seconde
            timer.Elapsed += OnTimedEvent; // La methode est exé à chaque intervalle
            timer.AutoReset = true; // La minuterie se réinitialisera après chaque déclenchement
            timer.Enabled = true; // Minuterie activée
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            randomValueTarget = Rdm.Next(0, Width);
                        
        }

        public override void Update()
        {

            if (randomValueCurrent < randomValueTarget)
            {
                randomValueCurrent++;
            }
            else if (randomValueCurrent > randomValueTarget)
            {
                randomValueCurrent--;
            }

            // Position + random de la taille 
            SetPosition((ball.Position.X - randomValueCurrent), Position.Y);

            if (Position.X <= 0 )
            {
                SetPosition(0, Position.Y);
            }
            if (Position.X >= Screen.Width + Texture.Width)
            {
                SetPosition(Screen.Width + Texture.Width/2, Position.Y);
            }

            base.Update();
        }

    }
}
