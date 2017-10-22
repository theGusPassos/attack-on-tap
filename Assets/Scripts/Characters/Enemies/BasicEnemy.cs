using AttackOnTap.Characters;
using AttackOnTap.Managers;
using System.Collections;
using UnityEngine;

namespace AttackOnTap.ArtificialIngelligence
{
    public class BasicEnemy : MonoBehaviour, ICharacter
    {
        private bool dead = false;
        private Animator animator;
        private IStateMachine stateMachine;

        private SpriteRenderer renderer;

        private void Awake()
        {
            stateMachine = GetComponent<IStateMachine>();
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
        }

        public void Die()
        {
            if (!dead)
            {
                stateMachine.Stop();
                animator.Play("die");
                dead = true;

                StartCoroutine(CharactersManager.FadeCharacter(GetComponent<SpriteRenderer>()));
            }
        }
    }
}
