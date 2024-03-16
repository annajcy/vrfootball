using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class WindowInstantFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 10f;
    public bool isFollowRotation = true;

    private Transform start;
    private Transform end;
    private float time = 0f;

    private void Start()
    {
        start = transform;
        end = target;
        time = 0f;
    }

    void Update()
    {
        if (end != target)
        {
            start = transform;
            end = target;
            time = 0f;
        }
        time += Time.deltaTime * followSpeed;
        transform.position =
            Vector3.Lerp(start.position, end.position, time);
        transform.rotation =
            Quaternion.Slerp(start.rotation, end.rotation, time);
    }
}
