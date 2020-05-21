using _Scripts.State_Machine.States;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour, IReset
    {
        private Vector3 initialPosition;

        private void Awake()
        {
            initialPosition = transform.position;
        }

        private void OnEnable()
        {
            GameState.OnExitGameStateEvent += Reset;
        }

        private void OnDisable()
        {
            GameState.OnExitGameStateEvent -= Reset;
        }

        public void Reset()
        {
            Debug.Log("Player Reset");
            transform.position = initialPosition;
        }
    }
}
