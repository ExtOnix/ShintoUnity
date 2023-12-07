using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    [SerializeField] Transform cameraTransform = null;
    [SerializeField, Range(1, 10)] float armLength = 5;
    [SerializeField, Range(1, 10)] float upDistance = 5;

    public Vector3 FinalPoint => transform.position + transform.forward * -armLength + transform.up * upDistance;
    private void LateUpdate()
    {
        UpdateCameraPosition(GetCameraAlpha());
    }

    float GetCameraAlpha()
    {
        bool _result = Physics.Raycast(new Ray(transform.position, transform.forward * -armLength), out RaycastHit _hitInfo, armLength);
        return _result ? (_hitInfo.distance / armLength) : 1;
    }

    void UpdateCameraPosition(float _alpha = 1)
    {
        if (!cameraTransform)
            return;
        cameraTransform.transform.position = Vector3.Lerp(transform.position, FinalPoint, _alpha);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = cameraTransform ? Color.red : Color.gray;
        Gizmos.DrawRay(transform.position, transform.forward * -armLength + transform.up * upDistance);
    }
}