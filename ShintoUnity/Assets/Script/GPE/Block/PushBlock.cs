using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : Block
{
    [SerializeField, Range(1, 100)] int length = 1;


    public override void Move(Vector3 _normal)
    {
        if (canMove) return;
        Vector3 _size = new Vector3(blockCollider.bounds.extents.x, .1f, blockCollider.bounds.extents.z);
        bool _hit = Physics.BoxCast(transform.position, _size, _normal, out RaycastHit _result, Quaternion.identity, length, layerMask);
        if (!_hit)
        {
            destination = transform.position + _normal * length;
            canMove = true;
        }
    }


    protected override void MoveTodestination()
    {
        base.MoveTodestination();
        if (MathUtils.CompareVector(transform.position, destination, new Vector3(.1f, .1f, .1f)))
            canMove = false;
    }

}
