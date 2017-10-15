using UnityEngine;

namespace AttackOnTap.ArtificialIngelligence
{
    public class BasicEnemyAI : MonoBehaviour
    {
        private Controllers.CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<Controllers.CharacterController>();
        }

        private void Update()
        {
            controller.SetDirectionalInput(new Vector2(-1, 0));
        }
    }
}

