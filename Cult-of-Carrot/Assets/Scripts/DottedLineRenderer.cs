using UnityEngine;

namespace Map
{
    public class DottedLineRenderer : MonoBehaviour
    {
        public bool scaleInUpdate = false;
        private LineRenderer lineRenderer;
        private Renderer rend;

        private void Start()
        {
            ScaleMaterial();
            enabled = scaleInUpdate;
        }

        public void ScaleMaterial()
        {
            lineRenderer = GetComponent<LineRenderer>();
            rend = GetComponent<Renderer>();
            rend.material.mainTextureScale =
                new Vector2(Vector2.Distance(lineRenderer.GetPosition(0),
                    lineRenderer.GetPosition(lineRenderer.positionCount - 1)) / lineRenderer.widthMultiplier, 1);
        }

        private void Update()
        {
            rend.material.mainTextureScale =
                new Vector2(Vector2.Distance(lineRenderer.GetPosition(0),
                    lineRenderer.GetPosition(lineRenderer.positionCount - 1)) / lineRenderer.widthMultiplier, 1);
        }
    }
}