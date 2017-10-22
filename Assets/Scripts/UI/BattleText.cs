using UnityEngine;
using UnityEngine.UI;

namespace AttackOnTap.UI
{
    public class BattleText : MonoBehaviour
    {
        public RectTransform textTransform;
        
        public Vector3[]    positions;
        private int         currentPos = 0;

        private Text text;
        private bool showing = false;
        private bool exiting = false;

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
        }

        public void SetBattleText(string text)
        {
            this.text.text = text;
            showing = true;
        }

        private void Update()
        {
            if (showing)
            {
                textTransform.localPosition 
                    = Vector3.Lerp(textTransform.localPosition, Vector3.zero, 0.1f);

                if (textTransform.localPosition == Vector3.zero)
                {
                    showing = false;
                    exiting = true;
                }
            }
            if (exiting)
            {
                textTransform.localPosition
                    = Vector3.Lerp(textTransform.localPosition, positions[currentPos], 0.1f);

                if (Vector3.Distance(textTransform.localPosition, positions[currentPos]) < 1)
                {
                    exiting = false;
                    currentPos = (currentPos == 1) ? 0 : 1;
                }
            }
        }
    }
}
