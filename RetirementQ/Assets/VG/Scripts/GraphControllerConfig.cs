using System;
using UnityEngine;

namespace Assets.VG.Scripts {
    [Serializable]
    public class GraphControllerConfig
    {
        public bool isRebuildOnStart;
        public GameObject barPrefab;
        public Transform graphOigin;
        public Vector2 thickness;
        public Vector2 offset;
        public Color minColor;
        public Color maxColor;
        public Color meanColor;
        public float responsiveness;
        public GameObject floorPrefab;
        public Vector2 floorPadding;
    
    }
}
