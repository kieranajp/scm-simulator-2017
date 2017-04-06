using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class LevelChange : MonoBehaviour
    {

        public string NextLevel = "Menu";

        private void Start () {
		
        }
	
        private void Update () {

            if (Input.anyKey)
            {
                SceneManager.LoadScene(NextLevel);
            }
        }
    }
}
