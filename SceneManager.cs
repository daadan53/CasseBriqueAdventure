using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    public class SceneManager
    {
        // Faire une liste qui va cointenir les scènes
        private List<Scene> _scenes;
        private Scene currentScene { get; set; }
        
        public SceneManager() 
        { 
            _scenes = new List<Scene>();
        }

        public void AddScenes(Scene scene)
        {
            _scenes.Add(scene);
        }

        // Fonction de changement de scène
        public void ChangeScene(int ID)
        {
            currentScene = _scenes[ID];
            currentScene.Load();
        }

        public void ResetScene()
        {
 
        }

        public void Load()
        {
            currentScene.Load();
        }

        public void Update()
        {
            currentScene.Update();
        }

        public void Draw()
        {
            currentScene.Draw();
        }
    }
}
