    using UnityEngine;

public class PlayerFollower : MonoBehaviour
{

    [SerializeField] private Jumping jumping;
    [SerializeField] private Transform target;

    private void Update()
    {
        var pos = transform.position;
        var position = target.position;
        pos.x = position.x;
        if (!jumping.grounded)
            pos.y = Mathf.Lerp(pos.y, position.y, 10f * Time.smoothDeltaTime);
        else
            pos.y = position.y;
        
        transform.position = pos;
        transform.localScale = target.localScale;
    }
}
