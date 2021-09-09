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
        [SerializeField] TextMeshProUGUI text;

        private void Update()
        {
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            float fillAmount = health.GetCurrentHealth() / health.GetMaxHealth();
            fill.fillAmount = fillAmount;

            text.text = health.GetCurrentHealth() + "/" + health.GetMaxHealth();
        }
    }
}