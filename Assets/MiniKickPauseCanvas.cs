using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MiniKickPauseCanvas : BaseCanvas
{
    public Button btnQuit;
    public Button btnResume;

    private void Start()
    {
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
    }

}
