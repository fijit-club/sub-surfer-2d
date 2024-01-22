using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Jumping jump;
    [SerializeField] private GameOverState gameOverState;
    [SerializeField] private CoinsHandler coinsHandler;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle") && jump.grounded)
        {
            EndGame();
        }
        else if (col.CompareTag("NotJumpable"))
        {
            EndGame();
        }
        else if (col.CompareTag("Coin"))
        {
            coinsHandler.GrabCoin();
            Destroy(col.gameObject);
        }
    }

    private void EndGame()
    {
        GameStateManager.ChangeState(gameOverState);
    }
}
