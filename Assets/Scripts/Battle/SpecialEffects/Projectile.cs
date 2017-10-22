using UnityEngine;

namespace AttackOnTap.Battle.SpecialEffects
{
    public class Projectile : MonoBehaviour
    {
        public float    projectileSpeed;
        public int      direction;
        public bool     destroyOnHit = false;

        private void Awake()
        {
            
        }

        private void Update()
        {
            transform.Translate(new Vector3(direction * projectileSpeed * Time.deltaTime, 0, 0));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
