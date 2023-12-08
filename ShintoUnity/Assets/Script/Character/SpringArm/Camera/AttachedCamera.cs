using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedCamera : MonoBehaviour
{
    Transform target = null;
    private void LateUpdate()
    {
        LookAtTarget(target);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void LookAtTarget(Transform _target)
    {
        transform.LookAt(_target);
    }
}
