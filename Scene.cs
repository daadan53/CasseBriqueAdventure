using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    public class Scene
    {
        public Rectangle Screen { get; private set; }
        protected ContentManager Content;
        protected GameWindow screen;
        protected SpriteFont FontMenu;
        protected SpriteBatch sb {  get; private set; }
        protected SceneManager sceneManager {  get; private set; }
        private int totalScene;
        private int currentSceneIndex;

        public Scene()
        {
            screen = ServiceLocator.GetService<GameWindow>();
            Content = ServiceLocator.GetService<ContentManager>();
            Screen = screen.ClientBounds;
            FontMenu = Content.Load<SpriteFont>("PixelFont");
            sb = ServiceLocator.GetService<SpriteBatch>();

        }

        private int GetTotalScene()
        {
            string levels = System.AppDomain.CurrentDomain.BaseDirectory + "Levels\\";

            if (Directory.Exists(levels))
            {
                string[] files = Directory.GetFiles(levels, "*.bbx");
                return files.Length;
            }
            else
            {
                return 1;
            }
        }

        public void LoadNextScene(int[,] pLvl)
        {
            Debug.WriteLine("je suis là"); 

            currentSceneIndex++;

            totalScene = GetTotalScene();
            if (currentSceneIndex > totalScene)
            {
                currentSceneIndex = 1;
            }

            ChangeNv(currentSceneIndex, pLvl);
        }

        private void ChangeNv(int pNum, int[,] pLvl)
        {

            // Appel du fichier level du dossier Levels
            string[] line = File.ReadAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "Levels\\" + "level_" + pNum  + ".bbx"); 

            // On recup ce qu'il y a d'écris
            for (int l = 0; l < line.Length; l++) 
            {
                string laLigne = line[l];
                for (int c = 0; c < laLigne.Length; c++)
                {
                    pLvl[l, c] = int.Parse(laLigne.Substring(c, 1));
                }
            }
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Load()
        {
            sceneManager = ServiceLocator.GetService<SceneManager>();
            currentSceneIndex = 0;
        }

        public virtual void Update()
        {
            

        }

        public virtual void Draw()
        {

        }
    }
}
