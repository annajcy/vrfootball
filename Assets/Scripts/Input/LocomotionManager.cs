using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocomotionManager : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float deadZone;
    public Transform target;
    public InputActionReference move;
    public InputActionReference rotate;

    private void OnRotate()
    {
        float result = rotate.action.ReadValue<Vector2>().x;
        if (Math.Abs(result) < deadZone) return;
        target.Rotate(Vector3.up, result * rotateSpeed * Time.deltaTime);
    }

    private void OnMove()
    {
        Vector2 result = move.action.ReadValue<Vector2>();
        if (Math.Abs(result.x) < deadZone && Math.Abs(result.y) < deadZone) return;
        if (Math.Abs(result.x) < deadZone) result.x = 0;
        if (Math.Abs(result.y) < deadZone) result.y = 0;
        Vector3 direction = new Vector3(result.x, 0f, result.y);
        target.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        OnRotate();
        OnMove();
    }
}
