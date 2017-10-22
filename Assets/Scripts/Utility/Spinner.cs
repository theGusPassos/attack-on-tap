using UnityEngine;

namespace AttackOnTap.Utility
{
    public class Spinner : MonoBehaviour
    {
        private bool    spinning = false;
        public float    spinSpeed;

        private void Update()
        {
            if (spinning)
            {
                transform.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
            }
        }

        public void StartSpinning()
        {
            spinning = true;
        }
        
        public void StopSpinning()
        {
            spinning = false;

            transform.localRotation = Quaternion.identity;
        }
    }
}
