using UnityEngine;

namespace AttackOnTap
{
    public class CharactersHolder : MonoBehaviour
    {
        public CharacterInfo[] characters;
    }

    [System.Serializable]
    public struct CharacterInfo
    {
        public string name;
        public GameObject obj;
    }
}
