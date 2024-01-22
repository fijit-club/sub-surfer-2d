using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float incrementSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float maxSpeed;
    
    public void ResetSpeed()
    {
        speed = initialSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);

        if (speed < maxSpeed)
            speed += Time.deltaTime * incrementSpeed;
    
        var direction = transform.InverseTransformDirection(transform.right);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
