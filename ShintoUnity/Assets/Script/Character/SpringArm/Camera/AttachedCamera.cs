using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedCamera : MonoBehaviour
{
    Transform target = null;
    private void Update()
    {
        LookAtTarget(target);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void LookAtTarget(Transform _target)
    {
        transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
    }
}
