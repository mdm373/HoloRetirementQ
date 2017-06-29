using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Assets.VG.Scripts {
    public class GraphSpawnController : MonoBehaviour {


        public HoloToolkit.Unity.InputModule.Cursor cursor;
        public GameObject startingPlane;
   
        private bool isChartPlaced;
        
        //based on hack from the https://github.com/ActiveNick/HoloModelShowcase/blob/master/Assets/Scripts/ModelPlacement.cs
        private GestureRecognizer recognizer;

        public void Start () {
            Debug.Log("Stat was kicked off");
            recognizer = new GestureRecognizer();
            recognizer.SetRecognizableGestures(GestureSettings.Tap);
        
            recognizer.TappedEvent += (source, tapCount, headRay) =>
            {
                HandleSpaceClicked();
            };

            Debug.Log(recognizer.IsCapturingGestures());
            recognizer.StartCapturingGestures();
            Debug.Log(recognizer.IsCapturingGestures());

            FindObjectOfType<EditorHandsInput>().OnGestured += HandleTap;
            FindObjectOfType<GesturesInput>().OnTap += HandleTap;

        }

        public void HandleTap()
        {
            HandleSpaceClicked();
        }

        private void HandleSpaceClicked()
        {

            Debug.Log("Involed HandlerSpaceClicked");
            if (!isChartPlaced) {
                Debug.Log("Chart Placed");
                isChartPlaced = true;
                Vector3 start = cursor.transform.position;
                GameObject instance = Instantiate(startingPlane);
                instance.transform.position = start;
                instance.transform.up = Vector3.up;
                Vector3 target = Camera.main.transform.position;
                target.y = instance.transform.position.y;
                instance.transform.LookAt(target);
            }
        }
    
    }
}

