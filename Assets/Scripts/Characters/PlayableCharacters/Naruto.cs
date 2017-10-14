using UnityEngine;

namespace AttackOnTap.Characters.PlayableCharacters
{
    public class Naruto : MonoBehaviour, IPlayableCharacter
    {
        public void BasicAttack()
        {
            print("Naruto basic attack");
        }

        public void RangedAttack()
        {
            print("naruto ranged attack");
        }

        public void SpecialAttack()
        {
            print("naruto special attack");
        }
    }
}
