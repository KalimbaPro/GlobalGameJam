using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void LateUpdate()
    {
        Vector3 desiredTarget = target.position + offset;
        Vector3 smoothedPosistion = Vector3.Lerp(transform.position, desiredTarget, smoothSpeed);
        transform.position = smoothedPosistion;
    }
}
