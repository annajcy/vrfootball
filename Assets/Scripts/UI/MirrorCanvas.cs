using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCanvas : BaseCanvas
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CanvasManager.Instance().Get<MirrorCanvas>().Hide();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
