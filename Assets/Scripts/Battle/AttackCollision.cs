using AttackOnTap.Managers;
using UnityEngine;

namespace AttackOnTap.Battle
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class AttackCollision : MonoBehaviour
    {
        public float    damage;
        public bool     constantDamage;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!constantDamage)
            {
                DealDamage(col.GetComponent<HealthPointSystem>());
            }
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (constantDamage)
            {
                DealDamage(col.GetComponent<HealthPointSystem>());
            }
        }

        public void DealDamage(HealthPointSystem sys)
        {
            if (sys != null)
            {
                SoundManager.Instance.PunchSound();
                sys.DealDamage(damage);
            }
        }
    }
}
