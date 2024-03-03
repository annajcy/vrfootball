using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCanvas : BaseCanvas
{
    private void Start()
    {
        CanvasManager.Instance().Get<MirrorCanvas>().Hide();
    }
}
