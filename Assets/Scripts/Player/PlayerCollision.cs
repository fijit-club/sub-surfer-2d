using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Jumping jump;
    [SerializeField] private GameObject gameOverUI;
    
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
    }

    private void EndGame()
    {
        gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
