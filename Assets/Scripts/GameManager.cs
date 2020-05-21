using _Scripts.State_Machine.Machines;
using _Scripts.State_Machine.States;
using Object_Pooler;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static StateMachine Machine;

    [SerializeField] private Rigidbody player;

    public static int TravelAmount;

    private bool isGrappling;

    #region Logic Handling via Input

    public delegate (Joint, int) Clicked(Vector3 arg);
    public static event Clicked OnClicked;
    
    public delegate void Attach(Vector3 position);
    public static event Attach OnAttach;
    
    public delegate void Score(int score);
    public static event Score OnScore;
    
    public delegate void Release();
    public static event Release OnRelease;

    #endregion

    public delegate void Dead();
    public static event Dead OnDead;

    private void Awake()
    {
        Machine = new StateMachine(new IdleState());
    }

    private void OnEnable()
    {
        GameState.OnExecuteGameStateEvent += HandleInput;
    }

    private void OnDisable()
    {
        GameState.OnExecuteGameStateEvent -= HandleInput;
    }

    private void Start()
    {
        ObjectPooler.SharedInstance.Pool();
    }

    private void Update()
    {
        Machine.Run();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isGrappling = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (isGrappling)
            {
                isGrappling = false;
                var clickResult = OnClicked?.Invoke(player.transform.position);
                if (clickResult.HasValue)
                {
                    var joint = clickResult.Value.Item1;
                    joint.connectedBody = player;
                    OnAttach?.Invoke(joint.transform.position);

                    TravelAmount = clickResult.Value.Item2 + 1;
                    OnScore?.Invoke(TravelAmount);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnRelease?.Invoke();
        }
    }

    public static void StopGame()
    {
        OnDead?.Invoke();
    }
}
