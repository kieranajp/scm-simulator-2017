using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class UndestroyableMusic : MonoBehaviour
    {
        private static GameObject _self;
        public string SceneDestroy = "Level_1";

        private void Awake()
        {
            if (_self == null)
            {
                _self = gameObject;
                SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(_self);
        }

        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == SceneDestroy)
            {
                Destroy(_self);
            }
        }
    }
}