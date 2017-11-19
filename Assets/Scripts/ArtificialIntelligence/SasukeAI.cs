using AttackOnTap.Characters.Boss;
using UnityEngine;

namespace AttackOnTap.ArtificialIntelligence
{
    public class SasukeAI : MonoBehaviour, IStateMachine
    {
        private Controllers.CharacterController controller;
        private Sasuke sasuke;
        private int layerMask;

        private SasukeAction currentAction = SasukeAction.START;

        private bool alive = true;

        private void Awake()
        {
            layerMask = LayerMask.GetMask("Hero");
            controller = GetComponent<Controllers.CharacterController>();
            sasuke = GetComponent<Sasuke>();
        }

        public void Die()
        {
            alive = false;
            controller.SetDirectionalInput(Vector2.zero);
        }

        private void Update()
        {
            if (alive)
            {
                switch (currentAction)
                {
                    case SasukeAction.START:
                        StartAction();
                        break;
                    case SasukeAction.MOVING:
                        Moving();
                        break;
                    case SasukeAction.SHARINGAN:
                        UseSharingan();
                        break;
                    case SasukeAction.CHIDORI_WALKING:
                        ChidoriWalking();
                        break;
                    case SasukeAction.CHIDORI_STANDING:
                        ChidoriStanding();
                        break;
                    case SasukeAction.MANGEKYOU:
                        Mangekyou();
                        break;
                    case SasukeAction.OUT:
                        Out();
                        break;
                }
            }
        }

        private float timeOut = 0;

        private void Moving()
        {
            sasuke.Disappear();

            if (timeOut > 1f)
            {
                timeOut = 0;
                gameObject.transform.position = nextPosition;
                sasuke.Appear();
                currentAction = nextAction;
            }

            timeOut += Time.deltaTime;
        }

        private SasukeAction nextAction;
        private Vector2 nextPosition;

        private void TriggerMovement(SasukeAction nextAction, Vector2 nextPosition)
        {
            this.nextAction = nextAction;
            this.nextPosition = nextPosition;

            currentAction = SasukeAction.MOVING;
        }

        public float startRange;

        private void StartAction()
        {
            sasuke.Disappear();

            controller.SetDirectionalInput(Vector2.left);
            controller.SetDirectionalInput(Vector2.zero);

            gameObject.transform.position += new Vector3(-50, 1, 0);

            sasuke.Appear();

            currentAction = SasukeAction.SHARINGAN;
        }
        
        private float timeInSharingan = 0;

        private void UseSharingan()
        {
            if (timeInSharingan > 4.5f)
            {
                sasuke.ShowSharingan();
                currentAction = SasukeAction.CHIDORI_WALKING;
            }

            timeInSharingan += Time.deltaTime;
        }

        private float timeInChidoriWalking = 0;
        public float chidoriWalkingRayRange;
        public float chidoriMaxDistance;

        private void ChidoriWalking()
        {
            Debug.DrawRay(transform.position, Vector2.left * chidoriWalkingRayRange);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, chidoriWalkingRayRange, layerMask);

            if (hit.collider != null)
            {
                if (Mathf.Abs(hit.collider.transform.position.x - transform.position.x) > chidoriMaxDistance)
                {
                    TriggerMovement(SasukeAction.CHIDORI_WALKING, new Vector2(hit.collider.transform.position.x + chidoriMaxDistance, gameObject.transform.position.y));
                }
            }

            if (timeInChidoriWalking > 3.5f)
            {
                sasuke.ChidoriWalking();
                currentAction = SasukeAction.CHIDORI_STANDING;
            }

            timeInChidoriWalking += Time.deltaTime;
        }

        private float timeInChidoriStanding = 0;

        private void ChidoriStanding()
        {
            if (timeInChidoriStanding > 5f)
            {
                sasuke.CastStandingChidori();
                currentAction = SasukeAction.MANGEKYOU;
            }

            timeInChidoriStanding += Time.deltaTime;
        }

        private float mangekyouTime = 0;
        private bool away = false;
        
        private void Mangekyou()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 200, layerMask);

            if (mangekyouTime > 10)
            {
                Debug.DrawRay(transform.position, Vector2.left * 200);

                if (hit.collider != null)
                {
                    if (Mathf.Abs(hit.collider.transform.position.x - transform.position.x) < 40)
                    {
                        TriggerMovement(SasukeAction.MANGEKYOU, new Vector2(hit.collider.transform.position.x + 40, gameObject.transform.position.y));
                        away = true;
                        return;
                    }
                }
            }

            if (away && mangekyouTime > 12)
            {
                sasuke.Mangekyou(hit.collider.transform.position);
                currentAction = SasukeAction.OUT;
            }

            mangekyouTime += Time.deltaTime;
        }

        private float timeToEnd = 0;

        private void Out()
        {
            if (timeToEnd > 3)
            {
                sasuke.Disappear();
                Stop();
                sasuke.Die();
            }
        }

        public void Stop()
        {
            alive = false;
        }

        private enum SasukeAction
        {
            START,
            SHARINGAN,
            MOVING,
            CHIDORI_WALKING,
            CHIDORI_STANDING,
            MANGEKYOU,
            OUT
        }
    }
}
