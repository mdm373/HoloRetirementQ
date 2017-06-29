using HoloToolkit.Unity;
using UnityEngine;

namespace Assets.VG.Scripts {
    public class WelcomeTextScript : MonoBehaviour {

        private bool isStartUp = true;

        public TextToSpeechManager textToSpeech;
    
        public void Update () {
            if (isStartUp)
            {
                isStartUp = false;
                textToSpeech.SpeakText("Please use the tap gesture on a flat surface to place the graph.");
            }

        }
    }
}
