using System;
using UnityEngine;

public interface IAIObject
{
    public void Move(Vector3 pos);
    public void StopMove();
    public void Attack();
    public void ChangeAction(EAction action);

    public Vector3 nowPos { get; }

    public Vector3 targetPos { get; }


}