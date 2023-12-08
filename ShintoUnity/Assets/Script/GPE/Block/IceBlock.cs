using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceBlock : Block
{

    public override void Move(Vector3 _normal)
    {
        Vector3 _size = new Vector3(blockCollider.bounds.extents.x, .1f, blockCollider.bounds.extents.z);
        bool _hit = Physics.BoxCast(transform.position, _size, _normal, out RaycastHit _result, Quaternion.identity, 100, layerMask);
        if (_hit)
            destination = _result.point;
        else destination = transform.position + _normal * 100;
        canMove = true;
    }



    protected override void MoveTodestination()
    {
        base.MoveTodestination();
        if (MathUtils.CompareVector(transform.position, destination, new Vector3(blockCollider.bounds.extents.x * 1.01f, blockCollider.bounds.extents.y * 1.01f, blockCollider.bounds.extents.z * 1.01f)))
            canMove = false;
    }
}
