
using UnityEngine;

namespace AttackOnTap.Characters.Enemies
{
    public class BasicEnemy : MonoBehaviour, ICharacter
    {
        public void Die()
        {
            print ("basic enemy is dead");
        }
    }
}
