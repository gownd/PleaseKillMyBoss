using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Combat;
using TMPro;
using UnityEngine.UI.ProceduralImage;

namespace Boss.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] Health health;

        [Header("Components")]
        [SerializeField] ProceduralImage fill;
        [SerializeField] ProceduralImage empty;
        [SerializeField] TextMeshProUGUI text;

        [Header("Colors")]
        [SerializeField] Color fillGreen;
        [SerializeField] Color emptyGreen;
        [SerializeField] Color fillRed;
        [SerializeField] Color emptyRed;

        private void Update()
        {
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            float fillAmount = health.GetCurrentHealth() / health.GetMaxHealth();
            fill.fillAmount = fillAmount;
            if (fillAmount <= 0.33)
            {
                fill.color = fillRed;
                empty.color = emptyRed;
            }
            else
            {
                fill.color = fillGreen;
                empty.color = emptyGreen;
            }

            text.text = health.GetCurrentHealth() + "/" + health.GetMaxHealth();
        }
    }
}