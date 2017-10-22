using AttackOnTap.Characters;
using UnityEngine;

namespace AttackOnTap.Managers
{
    public class CharactersManager : MonoBehaviour
    {
        public static bool canMove = true;
        public static CharactersManager Instance { get; set; }

        [SerializeField]
        private CharacterInfo       selectedCharacter;
        public static CharacterInfo mainCharacter;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InstantiateCharacter();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InstantiateCharacter()
        {
            Transform characterPos = GameObject.Find("Main Character Pos").transform;

            mainCharacter = selectedCharacter;
            mainCharacter.obj = Instantiate(mainCharacter.obj, characterPos.position, Quaternion.identity);

            mainCharacter.obj.name = mainCharacter.name;
        }

        public static void NotifyVictoryToChar()
        {
            canMove = false;
            mainCharacter.obj.GetComponent<IPlayableCharacter>().Celebrate();
        }
    }
}
