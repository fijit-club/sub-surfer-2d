using System.Collections;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public bool grounded = true;
    public bool inputJump;

    [SerializeField] private Transform playerSprite;
    [SerializeField] private float maxJumpSpeed = 7.0f;
    [SerializeField] private float fallSpeed = 12.0f;
    [SerializeField] private float maxJumpHeight = 3.0f;
    [SerializeField] private float gravity;
    [SerializeField] private PlayerElevationHandler elevationHandler;
    
    private Vector3 _groundPos;
    private float _currentJumpSpeed;
    private bool _setRotation;
    
    public void JumpTrigger()
    {
        if(grounded && !elevationHandler.enteredSlope)
        {
            _groundPos = playerSprite.localPosition;
            maxJumpHeight = playerSprite.localPosition.y + maxJumpHeight;
            inputJump = true;
            _currentJumpSpeed = maxJumpSpeed;
            _setRotation = false;
            StartCoroutine(nameof(Jump));
        }
    }

    private IEnumerator Jump()
    {
        while(true)
        {
            
            if(playerSprite.localPosition.y >= maxJumpHeight)
                inputJump = true;
            if (inputJump)
            {
                grounded = false;
                _currentJumpSpeed -= gravity * Time.deltaTime;
                playerSprite.Translate(Vector3.up * _currentJumpSpeed * Time.smoothDeltaTime);
                
                if (playerSprite.localPosition.y < _groundPos.y)
                {
                    var localPos = playerSprite.localPosition;
                    localPos.y = 0f;
                    playerSprite.localRotation = Quaternion.identity;
                    _setRotation = true;
                    playerSprite.parent = transform;
                    playerSprite.localPosition = localPos;
                    grounded = true;
                    StopAllCoroutines();
                }
            }
            else if(!inputJump)
            {
                //playerSprite.Translate(Vector3.down * fallSpeed * Time.smoothDeltaTime);
            }
			
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        if (_setRotation)
            playerSprite.localRotation = Quaternion.Lerp(playerSprite.localRotation, Quaternion.identity,
                10f * Time.deltaTime);
        else
        {
            // transform.localRotation = Quaternion.identity;
            // transform.rotation = Quaternion.identity;
        }
    }
}
