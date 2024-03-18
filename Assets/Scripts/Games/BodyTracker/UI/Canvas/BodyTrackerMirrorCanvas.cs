using System;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerMirrorCanvas : BaseCanvas
{
    public Transform respawnTransform;
    private void Awake()
    {
        onShow.AddListener(OnShow);
    }

    private void OnDestroy()
    {
        onShow.RemoveListener(OnShow);
    }

    private void OnShow()
    {
        transform.position = respawnTransform.position;
        transform.rotation = respawnTransform.rotation;
    }


}