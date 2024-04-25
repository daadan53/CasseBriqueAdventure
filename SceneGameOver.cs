using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    internal class SceneGameOver : Scene
    {

        public SceneGameOver() : base()
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
                sceneManager.ChangeScene(0);
            }
        }

        public override void Draw()
        {
            sb.DrawString(FontMenu, "PERDU", new Vector2(10, 10), Color.White);
        }
    }
}
