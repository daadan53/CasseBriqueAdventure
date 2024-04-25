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
    internal class SceneDeWin : Scene
    {

        public SceneDeWin() : base()
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
                sceneManager.ChangeScene(1);
            }
            //base.Update();
        }

        public override void Draw()
        {
            SpriteBatch sb = ServiceLocator.GetService<SpriteBatch>();

            sb.DrawString(FontMenu, "GAGNE !!", new Vector2(200, 300), Color.White);
        }
    }
}
