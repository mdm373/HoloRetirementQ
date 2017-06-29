using HoloToolkit.Examples.InteractiveElements;
using UnityEngine;

namespace Assets.VG.Scripts {
    public class FormattedSlider : MonoBehaviour {

        public SliderGestureControl slider;

        public TextMesh label;

        private float scalingFactor;

        public string labelPrefix;

        public void Start () {
            scalingFactor = slider.MinSliderValue;
            slider.SliderValue = slider.SliderValue - scalingFactor;
        }
	
        public void Update () {
            float value = Mathf.Round(slider.SliderValue + scalingFactor);
            label.text = labelPrefix + value.ToString(slider.LabelFormat);
        }
    }
}
