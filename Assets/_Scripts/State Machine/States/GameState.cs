using System;
using UnityEngine;

namespace _Scripts.State_Machine.States
{
    public class GameState : IState 
    {
        public delegate void EnterGameState();
        public delegate void ExecuteGameState();
        public delegate void ExitGameState();
    
        public static event EnterGameState OnEnterGameStateEvent;
        public static event ExecuteGameState OnExecuteGameStateEvent;
        public static event ExitGameState OnExitGameStateEvent;
    
        public void OnEnter() 
        {
            try 
            {
                if (OnEnterGameStateEvent != null)
                    OnEnterGameStateEvent.Invoke();
                else
                    EventExtension.ThrowMessage(nameof(OnEnterGameStateEvent));
            }
            catch (Exception e) 
            {
                EventExtension.PrintError(e);
            }    
        }
      
        public void OnExecute() 
        {
            try 
            {
                if (OnExecuteGameStateEvent != null)
                    OnExecuteGameStateEvent.Invoke();
                else
                    EventExtension.ThrowMessage(nameof(OnExecuteGameStateEvent));
            }
            catch (Exception e) 
            {
                EventExtension.PrintError(e);
            }      
        }
    
        public void OnExit() {
            try 
            {
                if (OnExitGameStateEvent != null)
                    OnExitGameStateEvent.Invoke();
                else
                    EventExtension.ThrowMessage(nameof(OnExitGameStateEvent));
            }
            catch (Exception e) 
            {
                EventExtension.PrintError(e);
            }  
        }
    }
}