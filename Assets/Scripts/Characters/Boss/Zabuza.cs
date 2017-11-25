using AttackOnTap.ArtificialIntelligence;
using AttackOnTap.Managers;
using UnityEngine;

namespace AttackOnTap.Characters.Boss
{
    public class Zabuza : MonoBehaviour, ICharacter, IBoss
    {
        private EnemyFactory factory;
        private Animator animator;

        public GameObject specialAttack;
        public GameObject waterDragon;

        public AudioClip basicSign;
        public AudioClip endSign;

        private bool dead = false;

        private ZabuzaAI ai;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            ai = GetComponent<ZabuzaAI>();
        }

        public void BasicSign()
        {
            SoundManager.Instance.PlaySoundEffect(basicSign);
        }

        public void EndSign()
        {
            SoundManager.Instance.PlaySoundEffect(endSign);
        }

        public void BasicAttack()
        {
            animator.Play("basicAttack");
        }

        public void ComboAttack()
        {
            animator.Play("comboAttack");
        }

        public void SpecialAttack1()
        {
            animator.Play("handSign");
        }

        public void SpecialAttack2()
        {
            animator.Play("handSign2");
        }

        public void ThrowSpecial1()
        {
            Instantiate(specialAttack).transform.position 
                = transform.position - new Vector3(10, 0, 0);
        }

        public void ThrowSpecialDragon()
        {
            animator.SetBool("done", false);

            Instantiate(waterDragon).transform.position
                = transform.position - new Vector3(10, 0, 0);
        }

        public void NotifyEndDragon()
        {
            animator.SetBool("done", true);
        }

        public void Die()
        {
            if (!dead)
            {
                ai.Die();
                animator.Play("die");
                factory.NotifyBossDeath();

                Destroy(gameObject, 1);

                dead = true;
            }
        }

        public void SetEnemyFactory(EnemyFactory enemyFactory)
        {
            factory = enemyFactory;
        }
    }
}
