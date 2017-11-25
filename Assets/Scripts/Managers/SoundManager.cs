using UnityEngine;

namespace AttackOnTap.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; set; }

        private AudioSource musicPlayer;
        private AudioSource sfxPlayer;
        private AudioSource punchPlayer;
        private AudioSource sfxPlayer2;

        public float musicTransitionSpeed;
        public float musicAppearanceSpeed;

        private bool isPlaying = false;
        private bool hasNext = false;
        private int lastMusic;

        private AudioClip nextToPlay;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            musicPlayer = GetComponents<AudioSource>()[0];
            sfxPlayer = GetComponents<AudioSource>()[1];
            punchPlayer = GetComponents<AudioSource>()[2];
            sfxPlayer2 = GetComponents<AudioSource>()[3];
        }

        private void Update()
        {
            if (hasNext)
            {
                if (musicPlayer.volume > 0)
                {
                    musicPlayer.volume -= musicTransitionSpeed * Time.deltaTime;
                }
                else
                {
                    musicPlayer.Stop();
                    musicPlayer.PlayOneShot(nextToPlay);
                    hasNext = false;
                }
            }
            else
            {
                if (isPlaying)
                {
                    if (musicPlayer.volume < 0.75f)
                    {
                        musicPlayer.volume += musicAppearanceSpeed * Time.deltaTime;
                    }
                }
            }
        }

        public void PlayClip(AudioClip clip)
        {
            if (isPlaying)
            {
                nextToPlay = clip;
                hasNext = true;
            }
            else
            {
                isPlaying = true;
                musicPlayer.PlayOneShot(clip);
            }
        }

        public void PlaySoundEffect(AudioClip clip)
        {
            if (sfxPlayer.isPlaying)
                sfxPlayer2.PlayOneShot(clip);
            else
                sfxPlayer.PlayOneShot(clip);
        }

        public AudioClip[] punchs;
        private int currentPunch = 0;

        public void PunchSound()
        {
            if (!punchPlayer.isPlaying)
            {
                punchPlayer.PlayOneShot(punchs[currentPunch]);

                if (currentPunch < punchs.Length - 1)
                {
                    currentPunch++;
                }
                else
                {
                    currentPunch = Random.Range(0, punchs.Length);
                }

            }
        }
    }
}
