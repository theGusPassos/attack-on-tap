using AttackOnTap.Managers;
using System.Collections;
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

        public AudioClip buttonSound;

        private bool dead = false;

        private string toCelebrate;

        public AudioClip dieSound;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Celebrate(int type)
        {
            toCelebrate = (type == 0) ? "celebrate" : "celebrateForEver"; 

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("narutoIdle"))
                animator.Play(toCelebrate); 
            else
                StartCoroutine("CelebrateInTime");
        }

        private IEnumerator CelebrateInTime()
        {
            while (animator.GetCurrentAnimatorStateInfo(0).IsName("narutoIdle"))
            {
                yield return new WaitForSeconds(0.1f);
            }

            animator.Play(toCelebrate);
            yield return 0;
        }
        
        public void Die()
        {
            if (!dead)
            {
                SoundManager.Instance.PlaySoundEffect(dieSound);
                CharactersManager.canMove = false;
                animator.Play("die");
                dead = true;
            }
        }

        public void BasicAttack()
        {
            SoundManager.Instance.PlaySoundEffect(buttonSound);

            if (CharactersManager.canMove)
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("narutoIdle"))
                    animator.Play("punching");
        }

        public void RangedAttack()
        {
            SoundManager.Instance.PlaySoundEffect(buttonSound);

            if (CharactersManager.canMove)
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("narutoIdle"))
                    animator.Play("rangedAttack");
        }

        public void SpecialAttack()
        {
            SoundManager.Instance.PlaySoundEffect(buttonSound);

            if (CharactersManager.canMove)
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
