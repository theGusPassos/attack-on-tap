using UnityEngine;

namespace AttackOnTap.Characters.PlayableCharacters
{
    public class Naruto : MonoBehaviour, IPlayableCharacter, ICharacter
    {
        private Animator animator;

        public GameObject kageBunshinSmoke;
        public GameObject rasengan;
        public GameObject throwableRasengan;
        public GameObject bijuuDamaCharger;
        public GameObject bijuuDama;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Die()
        {
            print("sasukeeeee");
        }

        public void BasicAttack()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("narutoIdle"))
                animator.Play("punching");
        }

        public void RangedAttack()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("narutoIdle"))
                animator.Play("rangedAttack");
        }

        public void SpecialAttack()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("narutoIdle"))
                animator.Play("specialAttack");
        }

        public void SummonKageBunshin()
        {
            Instantiate(kageBunshinSmoke, transform).transform.localPosition = Vector3.zero;
        }

        public void CastRasengan()
        {
            Instantiate(rasengan, transform).transform.localPosition = Vector3.zero;
        }

        public void ThrowRasengan()
        {
            Instantiate(throwableRasengan).transform.localPosition = transform.localPosition;
        }

        public void CastBijuuDama()
        {
            Instantiate(bijuuDamaCharger, transform).transform.localPosition 
                = new Vector3(8, -2, 0);
        }

        public void ThrowBijuudama()
        {
            Instantiate(bijuuDama).transform.localPosition = transform.position +
                new Vector3(14, -2, 0);
        }
    }
}
