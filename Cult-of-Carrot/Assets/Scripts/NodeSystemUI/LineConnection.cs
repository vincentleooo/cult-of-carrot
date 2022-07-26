using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class LineConnection
    {
        public LineRenderer lineRenderer;
        public MapNode from;
        public MapNode to;

        public LineConnection(LineRenderer lineRenderer, MapNode from, MapNode to)
        {
            this.lineRenderer = lineRenderer;
            this.from = from;
            this.to = to;
        }

        public void SetColor(Color color)
        {
            // Debug.Log("In setcolor");
            // lr.material.color = color;

            var gradient = lineRenderer.colorGradient;
            var colorKeys = gradient.colorKeys;
            
            for (var j = 0; j < colorKeys.Length; j++)
            {
                colorKeys[j].color = color;
            }

            gradient.colorKeys = colorKeys;
            lineRenderer.colorGradient = gradient;
        }
    }
}