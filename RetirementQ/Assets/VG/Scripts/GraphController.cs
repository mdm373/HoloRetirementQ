using System.Collections.Generic;
using UnityEngine;

namespace Assets.VG.Scripts {
    public class GraphController : MonoBehaviour {

        private class BarInstance
        {
            public GameObject bar;
            public Renderer renderer;
        }
        public GraphControllerConfig config;
        public GraphControllerData data;
        private List<List<BarInstance>> bars = new List<List<BarInstance>>();
        private GameObject floor;

        public void Start () {
            if (config.isRebuildOnStart)
            {
                Rebuild();
            }
        
        }

        public void Update()
        {
            Rebuild();
        }

        private void Rebuild()
        {
            for (int i = 0; i < data.values.Length; i++){
                GraphControllerDataPoint point = data.values[i];
                if(point.max <= point.min)
                {
                    point.max = point.min;
                }
                data.values[i].min = Mathf.Clamp(data.values[i].min, 0, 1);
                data.values[i].max = Mathf.Clamp(data.values[i].max, 0, 1);

                Rebuild(data.values[i].min, i, 0, config.minColor);
                Rebuild((data.values[i].min + data.values[i].max) / 2.0f, i, 1, config.meanColor);
                Rebuild(data.values[i].max, i, 2, config.maxColor);
            }
            for(int i = data.values.Length  ; i < bars.Count; i++)
            {
                Destroy(bars[i][0].bar);
                Destroy(bars[i][1].bar);
                Destroy(bars[i][2].bar);
                bars.RemoveAt(i);
            }

            if(floor == null)
            {
                floor = Instantiate(config.floorPrefab);
                floor.name = "floor";
            }
            float width = config.thickness.x * data.values.Length + (config.floorPadding.x * 2);
            float thick = config.thickness.y * 2 + (config.floorPadding.y * 2);
            Vector3 position = Vector3.zero;
            position.x = -config.floorPadding.x + (width * .5f);
            position.z = -config.floorPadding.y + (thick * .5f);
            Vector3 scale = floor.transform.localScale;
            scale.x = width;
            scale.z = thick;
            floor.transform.parent = config.graphOigin;
            floor.transform.localPosition = position;
            floor.transform.localScale = scale;
            floor.transform.localRotation = Quaternion.identity;

        }

        private void Rebuild(float dataPoint, int xIndex, int yIndex, Color color)
        {
            BarInstance bar = bars.Count > xIndex && bars[xIndex].Count > yIndex ? bars[xIndex][yIndex] : null;
            if(bar == null)
            {
                if (xIndex >= bars.Count)
                {
                    bars.Add(new List<BarInstance>());
                }
                GameObject barObj = Instantiate<GameObject>(config.barPrefab);
                barObj.transform.parent = config.graphOigin;
                barObj.transform.localRotation = Quaternion.identity;
                barObj.transform.localScale = Vector3.zero;
                barObj.name = "bar-" + xIndex + "-" + yIndex;
                bar = new BarInstance()
                {
                    bar = barObj,
                    renderer = barObj.GetComponentInChildren<Renderer>()
                };
                bars[xIndex].Add(bar);
            }
            bar.renderer.material.color = color;
            Vector3 position = Vector3.zero;
            position.x = config.offset.x * xIndex;
            position.z = config.offset.y * yIndex;
            bar.bar.transform.localPosition = position;
            Vector3 scale = bar.bar.transform.localScale;
            scale.y = Mathf.Lerp(scale.y, dataPoint, Time.deltaTime * config.responsiveness);
            scale.x = config.thickness.x;
            scale.z = config.thickness.y;
            bar.bar.transform.localScale = scale;
        }
    }
}
