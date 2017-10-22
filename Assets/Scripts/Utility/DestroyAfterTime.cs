using UnityEngine;

namespace AttackOnTap.Utility
{
    public class DestroyAfterTime : MonoBehaviour
    {
        public float timeToDestroy;

        private void Awake()
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}