using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererController : MonoBehaviour
    {
        private LineRenderer lineRenderer;

        private void Awake()
        {
            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.enabled = false;
            }
        }

        private void OnEnable()
        {
            GameManager.OnAttach += Attach;
            GameManager.OnRelease += Release;
        }

        private void OnDisable()
        {
            GameManager.OnAttach -= Attach;
            GameManager.OnRelease -= Release;
        }

        private void Update()
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.SetPosition(0, transform.position);
            }
        }

        private void Attach(Vector3 position)
        {
            lineRenderer.enabled = true;
            var positions = new[]
            {
                transform.position,
                position
            };

            lineRenderer.positionCount = positions.Length;
            for (var i = 0; i < positions.Length; i++)
            {
                lineRenderer.SetPosition(i, positions[i]);
            }
        }

        private void Release()
        {
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }
    }
}
