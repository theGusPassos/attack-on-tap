using UnityEngine;

namespace AttackOnTap.ArtificialIngelligence
{
    public class BasicEnemyAI : MonoBehaviour, IStateMachine
    {
        private Controllers.CharacterController controller;

        private BasicEnemyState state;

        public void Stop()
        {
            controller.SetDirectionalInput(Vector3.zero);
            state = BasicEnemyState.DEAD;
        }

        private void Awake()
        {
            controller = GetComponent<Controllers.CharacterController>();
        }

        private void Update()
        {
            if (state == BasicEnemyState.RUNNING)
            {
                Run();
            }
            else if (state  == BasicEnemyState.ATTACKING)
            {
                Attack();
            }
        }

        private void Run()
        {
            controller.SetDirectionalInput(new Vector2(-1, 0));
        }

        private void Attack()
        {

        }

        private enum BasicEnemyState
        {
            RUNNING,
            ATTACKING,
            DEAD
        }
    }
}

