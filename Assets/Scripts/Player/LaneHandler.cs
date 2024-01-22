using System;
using UnityEngine;

[Serializable]
public class LaneData
{
    public Transform[] laneTransform;
}

public class LaneHandler : MonoBehaviour
{
    public static int CurrentLane = 1;
    public static Transform CurrentLaneTransform;
    
    [SerializeField] private Transform laneHandlerTransform;
    [SerializeField] private Transform laneHandlerSpriteHolder;
    [SerializeField] private LaneData laneData;
    [SerializeField] private Jumping jumping;
    [SerializeField] private float swipeThreshold;
    [SerializeField] private Camera cam;
    
    private float _startTouchPosition;
    private float _endTouchPosition;

    private void Start()
    {
        CurrentLaneTransform = laneData.laneTransform[CurrentLane];
    }

    public void TouchedDown()
    {
        _startTouchPosition = Input.mousePosition.y / Screen.height;
    }

    public void TouchedUp()
    {
        _endTouchPosition = Input.mousePosition.y / Screen.height;
        CheckLane();
    }

    private void Update()
    {
        var pos = laneHandlerSpriteHolder.position;
        pos.y = Mathf.Lerp(pos.y, laneHandlerTransform.position.y, 10f * Time.deltaTime);
        laneHandlerSpriteHolder.position = pos;
        laneHandlerSpriteHolder.localScale = Vector3.Lerp(laneHandlerSpriteHolder.localScale,
            laneHandlerTransform.localScale, 10f * Time.deltaTime);
    }

    private void CheckLane()
    {
        if (_startTouchPosition - _endTouchPosition > swipeThreshold)
        {
            if (CurrentLane < laneData.laneTransform.Length - 1)
                CurrentLane++;
            UpdateLane();
        }
        else if (_startTouchPosition - _endTouchPosition < -swipeThreshold)
        {
            if (CurrentLane > 0)
                CurrentLane--;
            UpdateLane();
        }
        else
        {
            jumping.JumpTrigger();
        }
    }

    public void UpdateLane()
    {
        if (CurrentLane == 0)
            cam.depth = 1;
        else
            cam.depth = 3;
        
        laneHandlerTransform.position = laneData.laneTransform[CurrentLane].position;
        laneHandlerTransform.localScale = laneData.laneTransform[CurrentLane].localScale;
    }
}
