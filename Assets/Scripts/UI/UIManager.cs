using System;
using System.Collections.Generic;
using _Scripts.State_Machine.States;
using TMPro;
using UI.Controllers;
using UI.Types;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private List<CanvasController> canvasControllers;

        [Header("Value Controllers")] 
        [SerializeField] private List<UIValueController> valueControllers;

        [SerializeField] private TextMeshProUGUI score;

        private void OnEnable()
        {
            GameState.OnEnterGameStateEvent += Restart;

            GameManager.OnScore += AddScore;
            GameManager.OnDead += Dead;
        }

        private void OnDisable()
        {
            GameState.OnEnterGameStateEvent -= Restart;

            GameManager.OnScore -= AddScore;
            GameManager.OnDead -= Dead;
        }

        private void Start()
        {
            CanvasControllersHandler((int)UIType.Idle);
        }

        public void CanvasControllersHandler(int type)
        {
            var typeCount = Enum.GetNames(typeof(UIType)).Length;
            foreach (var canvasController in canvasControllers)
            {
                canvasController.Show((UIType)(type % typeCount));
            }
        }

        public void StateMachineHandler(int type)
        {
            var uiType = (UIType) type;
            switch (uiType)
            {
                case UIType.Idle:
                    GameManager.Machine.ChangeState(new IdleState());
                    break;
                case UIType.Game:
                    GameManager.Machine.ChangeState(new GameState());
                    break;
                case UIType.GameOver:
                    GameManager.Machine.ChangeState(new GameOverState());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // ReSharper disable once ParameterHidesMember
        private void AddScore(int score)
        {
            foreach (var valueController in valueControllers)
            {
                valueController.Increment(score);
            }
        }

        private void Restart()
        {
            foreach (var valueController in valueControllers)
            {
                valueController.SetValue(0);
            }
        } 


        private void Dead()
        {
            CanvasControllersHandler((int)UIType.GameOver);
            StateMachineHandler((int)UIType.GameOver);
        }
    }
}
