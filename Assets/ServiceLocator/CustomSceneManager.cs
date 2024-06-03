using UnityEngine.SceneManagement;

namespace ServiceLocator
{
    public class CustomSceneManager
    {

        public static void OpenScene(int sceneId, Scene thisScene)
        {
            CleanUpScene(thisScene);
            SceneManager.LoadScene(sceneId);

        }

        static void CleanUpScene(Scene scene)
        {
            
            ServiceLocator.RemoveSceneLocator(scene);
            
            
        }
        
    }
}