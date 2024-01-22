using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float incrementSpeed;
    [SerializeField] private float speed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);

        speed += Time.deltaTime * incrementSpeed;
    
        var direction = transform.InverseTransformDirection(transform.right);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
