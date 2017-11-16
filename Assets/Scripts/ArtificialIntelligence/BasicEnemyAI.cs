using UnityEngine;

namespace AttackOnTap.ArtificialIntelligence
{
    public class BasicEnemyAI : MonoBehaviour, IStateMachine
    {
        private Controllers.CharacterController controller;
        private BasicEnemy basicEnemy;

        public float hitDistance;

        private BasicEnemyState state;

        public void Stop()
        {
            controller.SetDirectionalInput(Vector3.zero);
            state = BasicEnemyState.DEAD;
        }

        private void Awake()
        {
            basicEnemy = GetComponent<BasicEnemy>();
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
            Debug.DrawRay(transform.position, Vector2.left * hitDistance, Color.green);
            int layerMask = LayerMask.GetMask("Hero");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, hitDistance, layerMask);

            controller.SetDirectionalInput(new Vector2(-1, 0));

            if (hit.collider != null)
            {
                controller.SetDirectionalInput(Vector2.zero);

                state = BasicEnemyState.ATTACKING;
            }
        }

        private void Attack()
        {
            basicEnemy.Attack();
        }

        private enum BasicEnemyState
        {
            RUNNING,
            ATTACKING,
            DEAD
        }
    }
}

