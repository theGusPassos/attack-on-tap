using UnityEngine;
using UnityEngine.SceneManagement;

namespace AttackOnTap.Utility
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private string sceneToLoad;

        public void StartScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
