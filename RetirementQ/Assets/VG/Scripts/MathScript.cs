using System;
using System.Collections.Generic;
using HoloToolkit.Examples.InteractiveElements;
using UnityEngine;

namespace Assets.VG.Scripts {
    public class MathScript : MonoBehaviour {

        public SliderGestureControl principalSlider;

        public SliderGestureControl riskToleranceSlider;

        public SliderGestureControl currentAgeSlider;

        public SliderGestureControl retirementAgeSlider;

        public SliderGestureControl yearlyContributionSlider;

        private GraphControllerData graphData;

        public GraphController graphController;

        private static readonly Dictionary<float, List<float>> MOCK_RETURN_RISK_TOLERACE_PERCENTAGES = new Dictionary<float, List<float>>
        {
            {0, new List<float>() {.03F, .035F } },
            {1, new List<float>() {.028F, .045F} },
            {2, new List<float>() {.02F, .06F}},
            {3, new List<float>() {.011F, .083F}},
            {4, new List<float>() {.00128F, .097F}}

        };

        public void Start () {
            graphData = graphController.data;
		
        }

        //TGA = Principal* (1 + .07/1)^(1*RTiY) <- Fancy looking compounded instrest calc makes a fun looking chart
        // Update is called once per frame
        public void Update() {
            float riskToleranceValue = (float)Mathf.Round(riskToleranceSlider.SliderValue);//+1)*Mathf.PI/10;
            float currentAgeValue = currentAgeSlider.SliderValue + currentAgeSlider.MinSliderValue;
            float retirementAgeValue = retirementAgeSlider.SliderValue + retirementAgeSlider.MinSliderValue;
            List<float> riskList = MOCK_RETURN_RISK_TOLERACE_PERCENTAGES[riskToleranceValue];
            float greatYearReturn = riskList[1];
            float badYearReturn = riskList[0];
            int ageDifference = (int)Math.Round(retirementAgeValue - currentAgeValue);

            if(ageDifference > 0)
            {
                graphData.values = new GraphControllerDataPoint[ageDifference];
                float maxReturnPerc = (MOCK_RETURN_RISK_TOLERACE_PERCENTAGES[4])[1];
                double maxReturnAmount = CalcFakeReturnCompoundedAnnually(maxReturnPerc, ageDifference);
                for (int i = 1; i<=ageDifference; i++)
                {
                    double maxValue = CalcFakeReturnCompoundedAnnually(greatYearReturn, i);
                    double minValue = CalcFakeReturnCompoundedAnnually(badYearReturn, i);
                    GraphControllerDataPoint point = new GraphControllerDataPoint {
                        max = (float) (maxValue/maxReturnAmount),
                        min = (float) (minValue/maxReturnAmount)
                    };
                    graphData.values[i - 1] = point;
                }


            }

        }

        double CalcFakeReturnCompoundedAnnually(float returnPerc, float years)
        {
            float principalValue = principalSlider.SliderValue;
            float annualIncrease = yearlyContributionSlider.SliderValue;

            float firstHalf = principalValue * Mathf.Pow(  (1 + ((returnPerc))), (years));
            float secondHalf =(annualIncrease * (Mathf.Pow( (1 + returnPerc), years) - 1) / returnPerc);

            return firstHalf + secondHalf;
        }
    }
}
