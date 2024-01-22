using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float _yOffset;
    
    private void Start()
    {
        _yOffset = transform.position.y - target.position.y;
    }

    private void Update()
    {
        var pos = new Vector3(target.position.x, target.position.y + _yOffset, -10f);
        transform.position = pos;
    }
}
