using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniKickContinueCanvas : BaseCanvas
{
    public TextMeshProUGUI textTitle;
    public Button btnContinue;
    public Button btnRetry;
    public Button btnQuit;

    private void Start()
    {
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
    }
}
