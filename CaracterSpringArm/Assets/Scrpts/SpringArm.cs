using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    [SerializeField] Camera cameraView = null;
    [SerializeField] float length = 2;
    [SerializeField] LayerMask layerMask;


    void Update()
    {
        UpdateCameraPosition();
    }

    float GetAlpha()
    {
        bool _hit = Physics.Raycast(new Ray(transform.position, -transform.forward), out RaycastHit _hitInfo, length, layerMask);
        if (_hit)
            return _hitInfo.distance / length;
        return 1;
    }


    void UpdateCameraPosition()
    {
        if (!cameraView) return;
        cameraView.transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward * length, GetAlpha());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * length);
    }
}
