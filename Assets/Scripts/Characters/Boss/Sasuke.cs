using AttackOnTap.ArtificialIntelligence;
using AttackOnTap.Managers;
using UnityEngine;

namespace AttackOnTap.Characters.Boss
{
    public class Sasuke : MonoBehaviour, ICharacter, IBoss
    {
        private Controllers.CharacterController controller;
        private EnemyFactory factory;
        private Animator animator;
        private SasukeAI sasuke;

        public AudioClip narutoMad;
        public AudioClip nande;
        public AudioClip narutoAndSasuke;

        public AudioClip basicSign;
        public AudioClip endSign;

        public AudioClip basicChidori;
        public AudioClip badassChidori;

        public AudioClip amaterasu;

        private bool dead = false;

        public GameObject amaterasuFlame;

        public void PlayBasicSign()
        {
            SoundManager.Instance.PlaySoundEffect(basicSign);
        }

        public void PlayEndSign()
        {
            SoundManager.Instance.PlaySoundEffect(endSign);
        }

        public void PlayChidori()
        {
            SoundManager.Instance.PlaySoundEffect(basicChidori);
        }

        public void PlayBadassChidori()
        {
            SoundManager.Instance.PlaySoundEffect(badassChidori);
        }

        private void Awake()
        {
            controller = GetComponent<Controllers.CharacterController>();
            animator = GetComponent<Animator>();
        }

        public void Disappear()
        {
            animator.Play("out");
        }

        private int times = 0;

        public void Appear()
        {
            if (times == 0)
            {
                SoundManager.Instance.PlaySoundEffect(narutoMad);
                times++;
            }

            animator.Play("appear");
        }

        public void ShowSharingan()
        {
            animator.Play("sharinganOn");
        }

        public void ChidoriWalking()
        {
            SoundManager.Instance.PlaySoundEffect(narutoAndSasuke);

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

        private Vector2 nextTarget;

        public void Mangekyou(Vector2 target)
        {
            nextTarget = target;
            animator.Play("amaterasuOn");
        }

        public void CastAmaterasu()
        {
            SoundManager.Instance.PlaySoundEffect(amaterasu);
            Instantiate(amaterasuFlame, nextTarget - new Vector2(0, 2), Quaternion.identity);
        }

        public void Die()
        {
            if (!dead)
            {
                SoundManager.Instance.PlaySoundEffect(nande);
                sasuke.Stop();
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