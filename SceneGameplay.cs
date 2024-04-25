using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    internal class SceneGameplay : Scene
    {
        Pad SprPad;
        Ball SprBall;
        Player SprPlayer;
        Door SprDoor;
        bool stickBall;
        public const int nbColonnes = 17;
        public const int nbLignes = 20;
        int[,] Lvl;
        private List<Brique> ListBriques;

        public SceneGameplay() : base()
        {
            SprBall = new Ball(Content.Load<Texture2D>("balle"), Screen);
            SprPad = new Pad(Content.Load<Texture2D>("Raquette"), Screen, SprBall); // On instancie la classe qui gère l'affichage des sprites EN LUI PASSANT LA TEXTURE POUR QUE ELLE LA GERE
            SprPlayer = new Player(Content.Load<Texture2D>("Player_Casse_Brique"), Screen);
            SprDoor = new Door(Content.Load<Texture2D>("Basic_Door_Pixel"), Screen);

            SprPad.SetPosition((Screen.Width/2) - (SprPad.Width/2), Screen.Height - SprPad.Height);
            SprBall.SetPosition(SprPad.CenterBall, SprPad.Position.Y - SprPad.Height);
            SprPlayer.SetPosition();
            SprDoor.SetPosition(Screen.Width/2 - 55, 20);

            SprBall.Speed = new Vector2(-3, -3);

            stickBall = false;
           
            Lvl = new int[nbLignes, nbColonnes];

            // On initialise/crée la brique
            ListBriques = new List<Brique>();

        }

        public void Reset()
        {
            SprPad.SetPosition((Screen.Width / 2) - (SprPad.Width / 2), Screen.Height - SprPad.Height);
            SprBall.SetPosition(SprPad.CenterBall, SprPad.Position.Y - SprPad.Height);
            SprPlayer.SetPosition();
            //SprDoor.SetPosition(Screen.Width / 2 - 55, 20);

            ListBriques.Clear();
        }

        public override void Load()
        {
            Reset();
            LoadNextScene(Lvl);

            // On en crée plsrs
            Texture2D texBrique; // La texture appliqué sur la bonne brique
            Texture2D[] texBriqueAll = new Texture2D[4]; // Un tableau qui prend deux textures différentes
            for (int t = 1; t <= 3; t++)
            {
                texBriqueAll[t] = Content.Load<Texture2D>("brique_" + t);
            }


            for (int l = 0; l < Lvl.GetLength(0); l++) //Longeur de la première dim du tableau
            {
                for (int c = 0; c < Lvl.GetLength(1); c++)
                {
                    int typeBrics = Lvl[l, c];
                    if (typeBrics != 0) // Si y'é un ptit 1 tu dessine, sinon non
                    {
                        texBrique = texBriqueAll[typeBrics];
                        Brique myBrique = new Brique(texBrique, Screen);

                        switch (typeBrics)
                        {
                            case 1:

                                break;

                            case 2:
                                // Initialiser la propriété de la brique deux et le reste dans update mais avant dans la classe brique
                                // Par ex : boom = false --> Et dasn le update on dit que quand elle est touché alors le bool passe à vrai
                                break;

                            case 3:
                                myBrique.isInvinsible = true;
                                break;

                            default: break;
                        }
                        myBrique.SetPosition(c * texBrique.Width, l * texBrique.Height);
                        ListBriques.Add(myBrique);
                    }

                }
            }

            base.Load();
            
        }
               
        public override void Update()
        {

            // VICTOIRE
            if (SprDoor.BoundingBox.Intersects(SprPlayer.BoundingBox))
            { 
                //SprPlayer.SetPosition(); // C'est pas ici que je dois reset la position
                sceneManager.ChangeScene(3);
                //LoadNextScene(Lvl);
            }
            // DEFAITE
            if (SprPlayer.Position.Y > Screen.Height || SprPlayer.BoundingBox.Intersects(SprBall.BoundingBox))
            {
                //SprPlayer.SetPosition();
                sceneManager.ChangeScene(2);
            }
           
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) 
            {
                stickBall = false;
            }

            SprBall.Update();
                                  
            SprPad.Update();

            SprPlayer.Update();

            SprPlayer.onTheSide = false;
            SprPlayer.onTheBric = false;
            SprPlayer.canJump = false;

            // On update toutes les briques
            for (int i = ListBriques.Count - 1; i>=0; i--)
            {
                bool Collision = false;
                Brique b = ListBriques[i];

                b.Update();

                if (!b.isEjected) 
                {
                    if (b.BoundingBox.Intersects(SprBall.NextPositionX())) // On a une collision horizontal
                    {
                        SprBall.BackSpeedX();
                        Collision = true;

                    }
                    if (b.BoundingBox.Intersects(SprBall.NextPositionY())) // On a une collision Vertical
                    {
                        SprBall.BackSpeedY();
                        Collision = true;
                    }

                    // On suppr
                    if (Collision && !b.isInvinsible)
                    {
                        //ListBriques.Remove(b);
                        b.Speed = new Vector2(SprBall.Speed.X, b.SpeedEject); // LA brique sera propulsé en fonction de l'angle d'arriver de la balle
                        b.isEjected = true;
                    }
                }
                
                if (b.Position.Y < 0)
                {
                    ListBriques.Remove(b);
                }


                // PLAYER 

                // On enregistre la position
                Vector2 oldPos = SprPlayer.Position;

                // On est SUR une brique
                if (SprPlayer.BoundingBox.Intersects(b.BoundingBox) && SprPlayer.Velocity.Y > 0)
                {

                    // Réajuster la position du joueur pour qu'il se trouve au-dessus de la brique
                    SprPlayer.Velocity = Vector2.Zero;
                    SprPlayer.onTheBric = true;


                    //break;
                }

                // Touche un côté
                else if (SprPlayer.Speed.X != 0 && b.BoundingBox.Intersects(SprPlayer.NextPositionX()))
                {
                   
                    SprPlayer.Speed = Vector2.Zero;
                    SprPlayer.Position = oldPos;
                    SprPlayer.onTheSide = true;
                                        
                }


                // Cogne la tete
                if (SprPlayer.Velocity.Y < 0)
                {
                    if (SprPlayer.NextPositionY((int)SprPlayer.Velocity.Y).Intersects(b.BoundingBox))
                    {
                        SprPlayer.Velocity = Vector2.Zero;
                        SprPlayer.Position = new Vector2(SprPlayer.Position.X, b.BoundingBox.Bottom);
                    }
                }

                // DEFAITE
                if (SprPlayer.BoundingBox.Intersects(b.BoundingBox) && b.isEjected)
                {
                    //SprPlayer.SetPosition();
                    sceneManager.ChangeScene(2);
                }
            }
       
            // Perso peut tomber ? Brique sous mes pieds 
            Vector2 feet = new Vector2(SprPlayer.Position.X + SprPlayer.Width/2, SprPlayer.Position.Y + SprPlayer.Height+1);

            foreach (Brique b in ListBriques)
            {
                // Si y'a une brique en dessous
                if(b.BoundingBox.Contains(feet))
                {
                    SprPlayer.onTheBric = true;
                    SprPlayer.Position = new Vector2(SprPlayer.Position.X, b.BoundingBox.Top - SprPlayer.Height);
                }
            }

            SprPlayer.Move();
                        
            
            // Si la balle tombe dans le vide
            if (stickBall) 
            {
                SprBall.SetPosition(SprPad.CenterBall - SprBall.Diametre, SprPad.Position.Y - SprPad.Height);
            }
            
            // Rebond de la balle
            if (SprPad.BoundingBox.Intersects(SprBall.NextPositionY()) && SprBall.Speed.Y > 0)
            {

                // Ajustement de la position de la balle pour qu'elle ne soit pas coincée dans le pad
                SprBall.SetPosition(SprBall.Position.X, SprPad.Position.Y - SprBall.Height);

                // Calcul de l'angle de rebond en fonction de la position où la balle a touché le pad
                float intersectX = (SprBall.Position.X + SprBall.Width / 2) - (SprPad.Position.X + SprPad.Width/2); // Distance
                float ratioX = intersectX / (SprPad.Width/2); // Ratio
                float bounceAngle = ratioX * (float)Math.PI / 3; // Angle de rebond limité à 60 degrés
                
                // Ajustement de la vitesse de la balle en fonction de l'angle de rebond
                float speed = SprBall.Speed.Length(); // Conservation de la vitesse initiale
                SprBall.Speed = new Vector2(speed * (float)Math.Sin(bounceAngle), SprBall.Speed.Y);
                SprBall.Speed = new Vector2(SprBall.Speed.X, -speed * (float)Math.Cos(bounceAngle)); // Inversion de la direction verticale pour rebondir vers le haut

            }


            if (SprBall.Position.Y >= Screen.Height)
            {
                stickBall = true;
            }
            
            
        }

        public override void Draw()
        {

            // On draw toutes les briques
            foreach (Brique b in ListBriques)
            {
                b.Draw(); // Vu que pad n'a pas de draw, on appel celui de son père
            }

            
            SprPad.Draw();
            SprBall.Draw();
            SprPlayer.Draw();
            SprDoor.Draw();

        }
    }
}
