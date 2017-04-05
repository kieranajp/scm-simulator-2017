using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class InstructionsScript : MonoBehaviour {

        private void Start () {
		
        }
	
        private void Update () {

            if (Input.anyKey)
            {
                SceneManager.LoadScene("Level_1");
            }
        }
    }
}
