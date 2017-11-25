using UnityEngine;
using UnityEngine.UI;

namespace AttackOnTap.Battle
{
    public class HealthBar : MonoBehaviour
    {
        private Slider barSlider;
        private float valueToGo;
        private bool valueChanged = false;

        private static Color fadeOutColor = new Color(0, 0, 0, 1);
        private Image[] childrenImages;
        private bool fadingOut = false;

        private void Awake()
        {
            barSlider = GetComponentInChildren<Slider>();
        }

        public void Initialize(float maxValue)
        {
            childrenImages = GetComponentsInChildren<Image>();

            foreach (Image img in childrenImages)
                img.color += new Color(0, 0, Mathf.Abs(img.color.a));

            fadingOut = false;

            barSlider.maxValue = maxValue;
            barSlider.value = maxValue;
        }

        public void SetValueToGo(float valueToGo)
        {
            this.valueToGo = valueToGo;
            valueChanged = true;
        }

        private void Update()
        {
            if (valueChanged)
            {
                barSlider.value = Mathf.Lerp(barSlider.value, valueToGo, 0.1f);

                if (barSlider.value == valueToGo)
                    valueChanged = false;
            }

            if (fadingOut)
            {
                FadeImagesOut();
            }
            else
            {
                FadeImagesIn();
            }
        }

        public void DisableHealthBar()
        {
            fadingOut = true;
        }

        private void FadeImagesOut()
        {
            foreach (Image img in childrenImages)
            {
                img.color -= fadeOutColor * Time.deltaTime;
            }
        }

        private void FadeImagesIn()
        {
            foreach (Image img in childrenImages)
            {
                if (img.color.a < 255)
                    img.color += fadeOutColor * Time.deltaTime;
            }
        }
    }
}