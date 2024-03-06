using System;
using UnityEngine;

public interface IAIObject
{
    public void Move(Vector3 dir);
    public void StopMove();
    public void Attack();
    public void ChangeAction(EAction action);


}