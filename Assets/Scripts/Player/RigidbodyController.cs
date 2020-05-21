using _Scripts.State_Machine.States;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyController : MonoBehaviour
    {
#pragma warning disable 108,114
        private Rigidbody rigidbody;
#pragma warning restore 108,114

        [SerializeField] private ParticleSystem particles;
        [SerializeField] private float velocityThreshold = 60f;

        [SerializeField] private float force = 10f;
        [SerializeField] private ForceMode mode;

        private Vector3 initialPosition;
        private bool isStart = false;

        private void Awake()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }
            LockPlayer();
        }

        private void OnEnable()
        {
            GameState.OnEnterGameStateEvent += UnlockPlayer;
            GameState.OnExecuteGameStateEvent += CheckVelocity;
            GameState.OnExitGameStateEvent += LockPlayer;

            GameManager.OnAttach += Attach;
            GameManager.OnRelease += Detach;
        }

        private void OnDisable()
        {
            GameState.OnEnterGameStateEvent -= UnlockPlayer;
            GameState.OnExecuteGameStateEvent -= CheckVelocity;
            GameState.OnExitGameStateEvent -= LockPlayer;

            GameManager.OnAttach -= Attach;
            GameManager.OnRelease -= Detach;
        }

        private void CheckVelocity()
        {
            if (rigidbody.velocity.magnitude > velocityThreshold)
            {
                if (particles.isPlaying)
                {
                    return;
                }
                particles.Play();
            }
            else
            {
                if (particles.isPlaying)
                {
                    particles.Stop();
                }
            }
        }

        private void LockPlayer()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.angularDrag = 0f;
            rigidbody.isKinematic = true;
            initialPosition = transform.position;
        }

        private void UnlockPlayer()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.angularDrag = 0f;
            
            rigidbody.isKinematic = false;
            isStart = true;
        }

        private void Attach(Vector3 position) => initialPosition = position;

        private void Detach()
        {
            var position = transform.position;
            var isForward = position.x > initialPosition.x;

            var distance = Vector3.Distance(initialPosition, position);

            if (isForward)
            {
                rigidbody.AddForce(force * distance * Vector3.right, mode);
            }
            else
            {
                if (isStart)
                {
                    isStart = false;
                    return;
                }
                rigidbody.AddForce(force * distance * Vector3.left, mode);
            }
        }
    }
}
