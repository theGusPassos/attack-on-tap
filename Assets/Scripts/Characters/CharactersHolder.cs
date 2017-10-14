using UnityEngine;
using System;

namespace AttackOnTap
{
    public class CharactersHolder : MonoBehaviour
    {
        public CharacterInfo[] characters;
    }

    [Serializable]
    public struct CharacterInfo
    {
        public string       name;
        public GameObject   obj;
        public Sprite       face;
        public AttackIcons  attackIcons;
    }

    [Serializable]
    public struct AttackIcons
    {
        public Sprite basicAttack;
        public Sprite rangedAttack;
        public Sprite specialAttack;
    }
}
