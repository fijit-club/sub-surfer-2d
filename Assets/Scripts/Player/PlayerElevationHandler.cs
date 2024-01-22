using UnityEngine;

public class PlayerElevationHandler : MonoBehaviour
{
    public bool enteredSlope;
    
    [SerializeField] private float rotationAngle;
    [SerializeField] private Jumping jumping;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform followerInSlope;
    
    private float _currentAngle;
    private Transform _lastRoad;

    public void ResetState()
    {
        _lastRoad = null;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Elevation Trigger Start") && !enteredSlope)
        {
            RotatePlayer(1, true, col.transform.parent);
        }
        else if (col.CompareTag("Elevation Trigger End") && enteredSlope)
        {
            RotatePlayer(-1, false, col.transform.parent);
        }
        else if (col.CompareTag("Elevation Start Down") && !enteredSlope)
        {
            RotatePlayer(-1, true, col.transform.parent);
        }
        else if (col.CompareTag("Elevation End Down") && enteredSlope)
        {
            RotatePlayer(1, false, col.transform.parent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Elevation Trigger End") || other.CompareTag("Elevation End Down"))
            GetElevation(other.transform.parent);
    }

    private void RotatePlayer(int dir, bool entered, Transform platform)
    {
        if (!jumping.grounded)
            playerSprite.parent = followerInSlope;
        _lastRoad = platform;
        enteredSlope = entered;
        if (entered)
            _currentAngle = platform.rotation.z;
        else
            _currentAngle = 0f;
    }

    private void Update()
    {
        var locRot = transform.rotation;
        locRot.z = Mathf.Lerp(locRot.z, _currentAngle, 10f * Time.deltaTime);
        transform.rotation = locRot;

        if (!enteredSlope && _lastRoad != null)
        {
            var pos = transform.position;
            var y = _lastRoad.GetComponent<ElevationRoadData>().elevation;
            pos.y = Mathf.Lerp(pos.y, y, 10f * Time.deltaTime);
            transform.position = pos;
        }
    }

    private void GetElevation(Transform col)
    {
        var pos = transform.position;
        var y = _lastRoad.GetComponent<ElevationRoadData>().elevation;
        pos.y = y;
        transform.position = pos;
    }
}
