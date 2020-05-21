using _Scripts.State_Machine.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Controllers
{
    [RequireComponent(typeof(Button))]
    public class StartGameOnClick : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void StartGame()
        {
            GameManager.Machine.ChangeState(new GameState());
        }
    }
}
