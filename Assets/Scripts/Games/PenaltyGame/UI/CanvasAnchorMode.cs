using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CanvasAnchorMode : MonoBehaviour
{
    public Transform strikerView;
    public Transform goalkeeperView;
    public Transform strikerAnchor;
    public Transform goalkeeperAnchor;
    public WindowFollow xrOriginWindowFollow;

    public void SetToStriker()
    {
        var transform1 = transform;
        var position = strikerView.position;
        var rotation = strikerView.rotation;
        transform1.position = position;
        transform1.rotation = rotation;
        xrOriginWindowFollow.target = strikerAnchor;

    }

    public void SetToGoalKeeper()
    {
        var transform1 = transform;
        var position = goalkeeperView.position;
        var rotation = goalkeeperView.rotation;
        transform1.position = position;
        transform1.rotation = rotation;
        xrOriginWindowFollow.target = goalkeeperAnchor;
    }

}
