using AttackOnTap.Characters.Boss;
using System;
using System.Collections;
using UnityEngine;

namespace AttackOnTap.ArtificialIntelligence
{
    public class ZabuzaAI : MonoBehaviour, IStateMachine
    {
        private Controllers.CharacterController controller;
        private Zabuza zabuza;
        private int layerMask;

        public bool alive = true;

        private ZabuzaAction currentAction = ZabuzaAction.SPECIAL_ATTACK2;

        private bool hasAttacked = false;

        public float meleeAttackRange;
        public float specialAttackOutRange1;
        public float specialAttackOutRange2;
        
        private void Awake()
        {
            layerMask = LayerMask.GetMask("Hero");
            controller = GetComponent<Controllers.CharacterController>();
            zabuza = GetComponent<Zabuza>();
        }

        private void Start()
        {
            currentAction = ZabuzaAction.SPECIAL_ATTACK1;
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
                    case ZabuzaAction.BASIC_ATTACK:
                        BasicAttack();
                        break;
                    case ZabuzaAction.COMBO_ATTACK:
                        ComboAttack();
                        break;
                    case ZabuzaAction.SPECIAL_ATTACK1:
                        SpecialAttack1();
                        break;
                    case ZabuzaAction.SPECIAL_ATTACK2:
                        SpecialAttack2();
                        break;
                }
            }
        }

        private void BasicAttack()
        {
            Debug.DrawRay(transform.position, Vector2.left * meleeAttackRange);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, meleeAttackRange, layerMask);

            controller.SetDirectionalInput(new Vector2(-1, 0));

            if (hit.collider != null)
            {
                controller.SetDirectionalInput(Vector2.zero);

                if (!hasAttacked)
                {
                    zabuza.BasicAttack();
                    hasAttacked = true;

                    StartCoroutine("ChooseAction");
                }
            }
        }

        private void ComboAttack()
        {
            Debug.DrawRay(transform.position, Vector2.left * meleeAttackRange);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, meleeAttackRange, layerMask);

            controller.SetDirectionalInput(new Vector2(-1, 0));

            if (hit.collider != null)
            {
                controller.SetDirectionalInput(Vector2.zero);

                if (!hasAttacked)
                {
                    zabuza.ComboAttack();
                    hasAttacked = true;

                    StartCoroutine("ChooseAction");
                }
            }
        }

        private void SpecialAttack1()
        {
            Debug.DrawRay(transform.position, Vector2.left * specialAttackOutRange1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, specialAttackOutRange1, layerMask);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, specialAttackOutRange1 + 15, layerMask);

            controller.SetDirectionalInput(new Vector2(1, 0));

            if (hit2.collider == null)
            {
                controller.SetDirectionalInput(new Vector2(-1, 0));
            }

            if (hit.collider == null && hit2.collider != null)
            {
                controller.SetDirectionalInput(Vector2.zero);
                controller.Flip();

                if (!hasAttacked)
                {
                    zabuza.SpecialAttack1();
                    hasAttacked = true;

                    StartCoroutine("ChooseAction");
                }
            }
        }

        private void SpecialAttack2()
        {
            Debug.DrawRay(transform.position, Vector2.left * specialAttackOutRange2);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, specialAttackOutRange2, layerMask);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, specialAttackOutRange2 + 15, layerMask);

            controller.SetDirectionalInput(new Vector2(1, 0));

            if (hit2.collider == null)
            {
                controller.SetDirectionalInput(new Vector2(-1, 0));
            }

            if (hit.collider == null && hit2.collider != null)
            {
                controller.SetDirectionalInput(Vector2.zero);
                controller.Flip();

                if (!hasAttacked)
                {
                    zabuza.SpecialAttack2();
                    hasAttacked = true;

                    StartCoroutine("WaitWaterDragon");
                }
            }
        }

        private IEnumerator WaitWaterDragon()
        {
            yield return new WaitForSeconds(4f);

            zabuza.NotifyEndDragon();

            StartCoroutine("ChooseAction");
        }

        private IEnumerator ChooseAction()
        {
            yield return new WaitForSeconds(2f);

            Array values = Enum.GetValues(typeof(ZabuzaAction));
            currentAction = (ZabuzaAction)values.GetValue(UnityEngine.Random.Range(0, values.Length));
                
            hasAttacked = false;
        }

        public void Stop()
        {
            alive = false;
        }

        private enum ZabuzaAction
        {
            BASIC_ATTACK,
            COMBO_ATTACK,
            SPECIAL_ATTACK1,
            SPECIAL_ATTACK2
        }
    }
}
