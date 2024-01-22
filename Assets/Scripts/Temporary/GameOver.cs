using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private MainMenuState mainMenuState;
    
    public void Restart()
    {
        GameStateManager.ChangeState(mainMenuState);
    }
}
