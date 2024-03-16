using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class WindowFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 10f;

    void Update()
    {
        transform.position =
            Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        transform.rotation =
                Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * followSpeed);
    }
}
