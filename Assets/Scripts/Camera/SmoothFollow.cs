using System;
using UnityEngine;

namespace Camera
{
    public class SmoothFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Rigidbody targetRigidbody;

        [SerializeField] private float smoothStep = 0.125f;
        [SerializeField] private Vector3 offset = new Vector3(0, 2f, -10f);
        [SerializeField] private Vector3 speedOffset = new Vector3(-1, -1f, -5f);

        [SerializeField] private float speedThreshold = 20f;

        private void FixedUpdate()
        {
            var position = target.position;
            var desiredPosition = targetRigidbody.velocity.magnitude < speedThreshold 
                ? position + offset
                : position + offset + speedOffset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothStep);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }
    }
}
