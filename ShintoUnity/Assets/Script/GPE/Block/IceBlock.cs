using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceBlock : Block
{

    public override void Move(RaycastHit _hitInfo)
    {
        Vector3 _dir = -(_hitInfo.normal);
        Debug.Log(blockCollider.bounds.extents.z * 1.1f);
        Vector3 _size = new Vector3(blockCollider.bounds.extents.x, .1f, blockCollider.bounds.extents.z);
        base.Move(_hitInfo);
        bool _hit = Physics.BoxCast(transform.position, _size, _dir, out RaycastHit _result, Quaternion.identity, 100, layerMask);
        if (_hit)
            destination = _result.point;
        else destination = transform.position + _dir * 100;
        canMove = true;
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(destination, new Vector3((blockCollider.size.x * 1.1f) * 2, (blockCollider.size.y * 1.1f) * 2, (blockCollider.size.z * 1.1f) * 2));
    }

}
