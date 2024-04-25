using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    internal class Brique : Sprite
    {
        public bool isInvinsible { get; set; }
        public bool isEjected {  get; set; }

        // Add ici la propriétés de la brique de type deux 
        public Brique(Texture2D pTexture, Rectangle pScreen) : base(pTexture, pScreen)
        {
            isInvinsible = false;
            isEjected = false;
            SpeedEject = -5;
        }

        public void Eject()
        {
            Speed = new Vector2(Speed.X, -10);
        }

        public override void Update()
        {
            Position += Speed;
            base.Update();
        }
    }
}
