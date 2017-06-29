using System.Collections.Generic;
using HoloToolkit.Examples.InteractiveElements;
using UnityEngine;

namespace Assets.VG {
    public class RiskToleranceFormatter : MonoBehaviour {

        public SliderGestureControl slider;

        public TextMesh label;

        private static readonly Dictionary<float, string> RISK_QUIZ_MAP = new Dictionary<float, string>
        {
            { 0, "Very Conservative" },
            { 1, "Conservative" },
            { 2, "Moderate" },
            { 3, "Aggressive" },
            { 4, "Very Aggressive" }
        };
    
        public void Update () {
            float value = Mathf.Round(slider.SliderValue);
            string riskValue = RISK_QUIZ_MAP[value];
            label.text = riskValue;
        }
    }
}
