using UnityEngine;

namespace AttackOnTap.Utility
{
    public class BackgroundController : MonoBehaviour
    {
        private RectTransform rectTransform;

        public float xSpeed;
        public float ySpeed;

        public Vector2 maxPos;
        public Vector2 minPos;

        private int xDirection = 1;
        private int yDirection = 1;

        private void Awake()
        {
            this.rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (xDirection > 0)
            {
                if (rectTransform.position.x > maxPos.x)
                {
                    xDirection = -xDirection;
                }
            }
            else
            {
                if (rectTransform.position.x < minPos.x)
                {
                    xDirection = -xDirection;
                }
            }

            rectTransform.position += new Vector3 (xSpeed * xDirection, 0);
        }
    }
}