using AttackOnTap.Managers;
using UnityEngine;

namespace AttackOnTap.Characters.Boss
{
    public class Sasuke : MonoBehaviour, ICharacter, IBoss
    {
        private Controllers.CharacterController controller;
        private EnemyFactory factory;
        private Animator animator;

        private bool dead = false;

        public GameObject amaterasuFlame;

        private void Awake()
        {
            controller = GetComponent<Controllers.CharacterController>();
            animator = GetComponent<Animator>();
        }

        public void Disappear()
        {
            animator.Play("out");
        }

        public void Appear()
        {
            animator.Play("appear");
        }

        public void ShowSharingan()
        {
            animator.Play("sharinganOn");
        }

        public void ChidoriWalking()
        {
            animator.Play("chidoriWalking");
        }

        public void Dash()
        {
            controller.SetDirectionalInput(Vector2.left);
        }

        public void StopDash()
        {
            controller.SetDirectionalInput(Vector2.zero);
        }

        public void CastStandingChidori()
        {
            animator.Play("handSigns");
        }

        public void Mangekyou(Vector2 target)
        {
            Instantiate(amaterasuFlame, target - new Vector2(0, 2), Quaternion.identity);
            animator.Play("amaterasuOn");
        }

        public void Die()
        {
            if (!dead)
            {
                factory.NotifyBossDeath();
                Disappear();
                dead = false;
            }
        }

        public void SetEnemyFactory(EnemyFactory enemyFactory)
        {
            factory = enemyFactory;
        }
    }
}