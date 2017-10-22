﻿using UnityEngine;
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
            if (Input.GetKeyDown(KeyCode.B))
                SetValueToGo(barSlider.value - 10);

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
        }

        public void DisableHealthBar()
        {
            childrenImages = GetComponentsInChildren<Image>();
            fadingOut = true;
        }

        private void FadeImagesOut()
        {
            foreach (Image img in childrenImages)
            {
                img.color -= fadeOutColor * Time.deltaTime;
            }
        }
    }
}