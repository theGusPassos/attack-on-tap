using AttackOnTap.Managers;
using UnityEngine;

namespace AttackOnTap.Utility
{
    public class StartCharForDebugging : MonoBehaviour
    {
        private void Awake()
        {
            CharactersManager.Instance.InstantiateCharacter();
        }
    }
}

