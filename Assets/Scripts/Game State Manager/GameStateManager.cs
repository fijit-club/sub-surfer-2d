using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static IState CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = FindObjectOfType<MainMenuState>();
        CurrentState.OnEnter();
    }

    public static void ChangeState(IState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}