using System;

namespace Assets.VG.Scripts {
    [Serializable]
    public class GraphControllerData  {

        public GraphControllerDataPoint[] values;
    }

    [Serializable]
    public class GraphControllerDataPoint
    {
        public float min;
        public float max;
    }
}