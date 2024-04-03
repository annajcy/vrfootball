using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoalkeeperController : PlayerController
{
    public Transform ballTransform;
    public Transform goalTransform;
    private void OnEnable()
    {
        Respawn();
        SwitchPose(GoalkeeperAction.Ready);
    }

    public GoalkeeperAction GetPredictedAction(Vector3 speed)
    {
        animator.speed = Random.Range(1.0f, 1.5f);
        Ray ray = new Ray(ballTransform.position, speed);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit))
        {
            if (rayCastHit.collider.gameObject.name == "ScoreSurface")
            {
                Vector3 position = rayCastHit.point;
                Vector3 saveVector = position - goalTransform.position;
                if (saveVector.x < 1.5 && saveVector.x > -1.5)
                {
                    if (saveVector.y < 0.7)
                        return GoalkeeperAction.CenterDown;
                    else if (saveVector.y >= 0.7 && saveVector.y <= 1.7)
                        return GoalkeeperAction.CenterMid;
                    else
                        return GoalkeeperAction.CenterUp;

                }
                else if (saveVector.x <= -1.5)
                {
                    if (saveVector.y < 0.7)
                        return GoalkeeperAction.LeftDown;
                    else if (saveVector.y >= 0.7 && saveVector.y <= 1.7)
                        return GoalkeeperAction.LeftMid;
                    else
                        return GoalkeeperAction.LeftUp;
                }
                else if (saveVector.x >= 1.5)
                {
                    if (saveVector.y < 0.7)
                        return GoalkeeperAction.RightDown;
                    else if (saveVector.y >= 0.7 && saveVector.y <= 1.7)
                        return GoalkeeperAction.RightMid;
                    else
                        return GoalkeeperAction.RightUp;
                }
            }
        }
        return 0;
    }

    [ContextMenu(nameof(GetPredictedAction))]
    public GoalkeeperAction GetPredictedAction()
    {
        Vector3 speed = new Vector3(0.2f, 0, -1);
        animator.speed = Random.Range(1.0f, 1.5f);
        Ray ray = new Ray(ballTransform.position, speed);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit))
        {
            if (rayCastHit.collider.gameObject.name == "ScoreSurface")
            {
                Vector3 position = rayCastHit.point;
                Vector3 saveVector = position - goalTransform.position;
                if (saveVector.x < 1.5 && saveVector.x > -1.5)
                {
                    if (saveVector.y < 0.7)
                        return GoalkeeperAction.CenterDown;
                    else if (saveVector.y >= 0.7 && saveVector.y <= 1.7)
                        return GoalkeeperAction.CenterMid;
                    else
                        return GoalkeeperAction.CenterUp;

                }
                else if (saveVector.x <= -1.5)
                {
                    if (saveVector.y < 0.7)
                        return GoalkeeperAction.LeftDown;
                    else if (saveVector.y >= 0.7 && saveVector.y <= 1.7)
                        return GoalkeeperAction.LeftMid;
                    else
                        return GoalkeeperAction.LeftUp;
                }
                else if (saveVector.x >= 1.5)
                {
                    if (saveVector.y < 0.7)
                        return GoalkeeperAction.RightDown;
                    else if (saveVector.y >= 0.7 && saveVector.y <= 1.7)
                        return GoalkeeperAction.RightMid;
                    else
                        return GoalkeeperAction.RightUp;
                }
            }
        }
        return 0;
    }

    public void SwitchPose(GoalkeeperAction goalkeeperAction)
    {
        animator.SetInteger(state, (int)goalkeeperAction);
    }

    public void React(Vector3 speed)
    {
        SwitchPose(GetPredictedAction(speed));
    }
}