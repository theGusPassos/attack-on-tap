using AttackOnTap.Characters;
using AttackOnTap.Managers;
using System.Collections;
using UnityEngine;

namespace AttackOnTap.ArtificialIntelligence
{
    public class BasicEnemy : MonoBehaviour, ICharacter
    {
        private bool dead = false;
        private Animator animator;
        private IStateMachine stateMachine;

        private SpriteRenderer renderer;

        private string[] attackList = {"kick", "punch"};

        private void Awake()
        {
            stateMachine = GetComponent<IStateMachine>();
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
        }

        public void Attack()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                animator.Play(ChooseAttack());
        }

        private string ChooseAttack()
        {
            return attackList[Random.Range(0, attackList.Length)];
        }

        public void Die()
        {
            if (!dead)
            {
                stateMachine.Stop();
                animator.Play("die");
                dead = true;

                Destroy(gameObject, 2);
                StartCoroutine(CharactersManager.FadeCharacter(GetComponent<SpriteRenderer>(), gameObject));
            }
        }
    }
}
