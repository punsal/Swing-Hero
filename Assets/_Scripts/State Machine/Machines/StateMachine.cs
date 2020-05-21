using System;
using UnityEngine;

namespace _Scripts.State_Machine.Machines {
    /// <summary>
    /// State machine for game-play.
    /// </summary>
    public class StateMachine {
        private IState currentState;
        private IState previousState;

        #region Next State

        public delegate void NextState(IState state);

        public static event NextState OnNextStateEvent;

        /// <summary>
        /// Call this trigger from IState:OnExit().
        /// </summary>
        /// <param name="state">Takes a state to run in StateMachine</param>
        /// <seealso cref="StateMachine"/>
        public static void TriggerNextState(IState state) {
            try {
                if (OnNextStateEvent != null)
                    OnNextStateEvent.Invoke(state);
                else
                    EventExtension.ThrowMessage(nameof(OnNextStateEvent));
            }
            catch (Exception e) {
                EventExtension.PrintError(e);
            }
        }

        #endregion

        #region Previous State

        public delegate void PreviousStateEvent();

        public static event PreviousStateEvent OnPreviousState;

        public static void TriggerPreviousState() {
            try {
                if (OnPreviousState != null)
                    OnPreviousState.Invoke();
                else
                    EventExtension.ThrowMessage(nameof(OnPreviousState));
            }
            catch (Exception e) {
                EventExtension.PrintError(e);
            }
        }

        #endregion

        public StateMachine(IState state) {
            ChangeState(state);
            OnNextStateEvent += ChangeState;
            OnPreviousState += BackToPreviousState;
        }

        ~StateMachine() {
            OnNextStateEvent -= ChangeState;
            OnPreviousState -= BackToPreviousState;
        }

        public void ChangeState(IState state) {
            if (currentState?.GetType() != state?.GetType()) {
                currentState?.OnExit();
                previousState = currentState;

                currentState = state;
                currentState?.OnEnter();
            }
            else {
                Debug.Log(GetType().Name + ".ChangeState : void param(state" + state.GetType().Name +
                          ") is equal to currentState.");
            }
        }

        public void BackToPreviousState() {
            ChangeState(previousState);
        }

        public void Run() {
            currentState?.OnExecute();
        }
    }
}