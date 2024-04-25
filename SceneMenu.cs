using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    internal class SceneMenu : Scene
    {

        public SceneMenu() : base()
        {
            
        }

        public override void Load()
        {
            base.Load();
        }

        public override void Update()
         {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // On lui passe le scnène manager 
                sceneManager.ChangeScene(1);
            }
            //base.Update();
        }

        public override void Draw()
        {
            
            sb.DrawString(FontMenu, "MENU", new Vector2(10, 10), Color.White);
        }
    }
}
