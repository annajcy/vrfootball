using System.Collections.Generic;
using UnityEngine;

public class MiniKickGameUpgradeState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        false, true, false, true, true
    };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<MiniKickGameCanvasSet>().SetState(defaultCanvasState);
        var miniKickGameUpgradeCanvas = CanvasSetManager.Instance().
            GetCanvasSet<MiniKickGameCanvasSet>().
            GetCanvas<MiniKickGameUpgradeCanvas>();
        var level = MiniKickGameManager.Instance().GetLevel();
        var score = MiniKickGameManager.Instance().GetScore();
        var nowState = MiniKickGameManager.Instance().GetTimerUpState();
        if (nowState == LevelCheckState.Pass)
        {
            miniKickGameUpgradeCanvas.titleText.text = "通过level " + level;
        }
        else if (nowState == LevelCheckState.Failed)
        {
            miniKickGameUpgradeCanvas.titleText.text = "未通过level " + level;
            miniKickGameUpgradeCanvas.upgradeButton.gameObject.SetActive(false);
        }
        else if (nowState == LevelCheckState.Maxed)
        {
            miniKickGameUpgradeCanvas.titleText.text = "已通关, 得分是: " + score;
            miniKickGameUpgradeCanvas.upgradeButton.gameObject.SetActive(false);
        }
    }

    public override void OnUpdate() { }

    public override void OnQuit() { }
}