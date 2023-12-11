using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    [SerializeField, Range(1, 10), Header("Spring Arm Settings")] float length = 5;
    [SerializeField, Range(0, 360)] float angle = 0;
    [SerializeField, Range(0, 10)] float height = 2;
    [SerializeField, Header("Camera")] AttachedCamera attachedCamera = null;

    public AttachedCamera AttachedCamera => attachedCamera;
    public float Angle { get { return angle; } set { angle = value; } }
    private void Start()
    {
        Init();
    }

    private void LateUpdate()
    {
        if (!attachedCamera)
            return;
        //UpdateCameraPosition();
    }
    void Init()
    {
        if (!attachedCamera)
            return;
        attachedCamera.SetTarget(transform);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * length, height, Mathf.Sin(angle * Mathf.Deg2Rad) * length));
        Gizmos.color = Color.red;
    }

    public void UpdateCameraPosition()
    {
        float _rad = angle * Mathf.Deg2Rad;
        attachedCamera.transform.position = transform.position + new Vector3(Mathf.Cos(_rad) * length, height, Mathf.Sin(_rad) * length);
    }
}