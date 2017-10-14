using UnityEngine;

namespace AttackOnTap.Characters.PlayableCharacters
{
    public class Naruto : MonoBehaviour, IPlayableCharacter, ICharacter
    {
        public void Die()
        {
            print("sasukeeeee");
        }

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
