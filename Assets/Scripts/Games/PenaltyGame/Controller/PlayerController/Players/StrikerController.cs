using UnityEngine;

public class StrikerController : PlayerController
{
    public Transform target;
    public BallController ballController;

    private void OnEnable()
    {
        Respawn();
        SwitchPose(StrikerAction.Ready);
    }

    public void KickBall()
    {
        int strength = Random.Range(10, 20);
        ballController.KickToTarget(target, strength);
        Debug.Log("Ball Kicked");
    }

    public void GenerateRandomGoalTarget()
    {
        target.localPosition = Vector3.zero;
        float heightOffset = Random.Range(0.0f, 2.8f);
        float widthOffset = Random.Range(-3.9f, 3.9f);
        var position = target.position;
        position.x += widthOffset;
        position.y += heightOffset;
        target.position = position;
    }

    public void SwitchPose(StrikerAction strikerAction)
    {
        animator.SetInteger(state, (int)strikerAction);
    }
}