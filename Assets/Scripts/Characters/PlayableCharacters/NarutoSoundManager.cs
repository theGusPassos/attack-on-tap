using AttackOnTap.Managers;
using UnityEngine;

namespace AttackOnTap.Characters.PlayableCharacters
{
    public class NarutoSoundManager : MonoBehaviour
    {
        public AudioClip missPunch;
        public AudioClip kagebunshi;
        public AudioClip fuutonRasengan;

        public void PlayPunch()
        {
            SoundManager.Instance.PlaySoundEffect(missPunch);
        }

        public void PlayKagebunshi()
        {
            SoundManager.Instance.PlaySoundEffect(kagebunshi);
        }

        public void RasenganSound()
        {
            SoundManager.Instance.PlaySoundEffect(fuutonRasengan);
        }
    }
}
