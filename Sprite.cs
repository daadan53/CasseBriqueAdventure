using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    public class Sprite
    {
        protected Rectangle Screen;
        public Texture2D Texture;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public int SpeedEject { get; set; }
        public Rectangle BoundingBox { get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        } // Boite de collision

        //Renvoie la taille de la texture
        public int Height
        {
            get
            {
                return Texture.Height;
            }
        }
        public int Width
        {
            get
            {
                return Texture.Width;
            }
        }
        public int Diametre
        {
            get
            {
                return Texture.Width/2;
            }
        }


        public Sprite(Texture2D pTexture, Rectangle pScreen)
        {
            Texture = pTexture;
            Screen = pScreen;
            
        }

        public float CenterBall
        {
            get
            {
                return Position.X + Width / 2;
            }
        }

        public void SetPosition() 
        { 
            Position = new Vector2(Screen.Width - 20, Screen.Height / 2);
        }

        // Fonction polymorphe pour eviter de faire new vector deux à chaque fois qu'on qappel la méthode
        public void SetPosition(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
            
        }

        // On crée une bounds devant notre balle qui va anticiper nos collisions 
        public Rectangle NextPositionX()
        {
            Rectangle nextPosition = BoundingBox;
            nextPosition.Offset(new Point((int)Speed.X, 0)); // Décale la new bounding box de speed, c'est pareil que ca --> nexPosition.X = nextPosition.X + Vitesse.X
            return nextPosition;
            
        }
        public Rectangle NextPositionX(int decallage)
        {
            Rectangle nextPosition = BoundingBox;
            nextPosition.Offset(new Point(decallage, 0)); // Décale la new bounding box de speed, c'est pareil que ca --> nexPosition.X = nextPosition.X + Vitesse.X
            return nextPosition;

        }
        public Rectangle NextPositionY()
        {
            Rectangle nextPosition = BoundingBox;
            nextPosition.Offset(new Point(0, (int)Speed.Y));
            return nextPosition;
        }

        public Rectangle NextPositionY(int decallage)
        {
            Rectangle nextPosition = BoundingBox;
            nextPosition.Offset(new Point(0, decallage));
            return nextPosition;
        }

        public void BackSpeedX()
        {
            Speed = new Vector2(-Speed.X, Speed.Y);
        }

        public void BackSpeedY()
        {
            Speed = new Vector2(Speed.X, -Speed.Y);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            SpriteBatch sb = ServiceLocator.GetService<SpriteBatch>();

            sb.Draw(Texture, Position, Color.White);
        }

    }
}
