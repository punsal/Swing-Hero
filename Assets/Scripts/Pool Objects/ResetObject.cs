using System;
using _Scripts.State_Machine.States;
using Interfaces;
using UnityEngine;

namespace Pool_Objects
{
    public class ResetObject : MonoBehaviour, IReset
    {
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
            gameObject.SetActive(false);
        }
    }
}
