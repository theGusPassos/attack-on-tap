using UnityEngine;

namespace AttackOnTap.Camera
{
    public class CameraSingleton : MonoBehaviour
    {
        private static bool created = false;

        private void Awake()
        {
            if (!created)
                created = true;
            else
                Destroy(gameObject);
            
            DontDestroyOnLoad(gameObject);
        }
    }
}