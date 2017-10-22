using AttackOnTap.Physics;
using UnityEngine;

namespace AttackOnTap.Controllers
{
    public class CharacterController : Controller2D
    {
        public float maxJumpHeight = 4f;
        public float minJumpHeight = 1f;
        public float timeToJumpApex = 0.4f;
        public float movespeed = 6;

        private Vector2 directionalInput;
        private bool facingRight = true;

        private float gravity;
        private float maxJumpVelocity;
        private float minJumpVelocity;

        private float accelerationTimeAirborne = 0.2f;
        private float accelerationTimeGrounded = 0.1f;

        private Vector3 velocity;
        private float velocityXSmoothing;

        private Animator animator;

        public override void Start()
        {
            base.Start();

            gravity = -(2 * maxJumpHeight / Mathf.Pow(timeToJumpApex, 2));
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

            animator = GetComponent<Animator>();
        }

        public void Update()
        {
            CalculateVelocity();

            Move(velocity * Time.deltaTime, directionalInput);
            
            animator.SetFloat("speed", Mathf.Abs(directionalInput.x));

            if (collisions.above || collisions.below)
            {
                velocity.y = 0;
            }
        }

        public void SetDirectionalInput(Vector2 input)
        {
            directionalInput = input;
            TestFlip();
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = directionalInput.x * movespeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
                (collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
        }

        private void TestFlip()
        {
            if (facingRight && directionalInput.x < 0 ||
                !facingRight && directionalInput.x > 0)
            {
                Flip();
            }
        }

        public void Flip()
        {
            facingRight = !facingRight;

            transform.localScale = new Vector3(-transform.localScale.x,
                transform.localScale.y,
                transform.localScale.y);
        }

        public void OnJumpInputDown()
        {
            if (collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
        }

        public void OnJumpInputUp()
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }
    }
}