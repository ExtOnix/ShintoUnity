using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : Block
{
    [SerializeField, Range(1, 100)] int length = 1;


    public override void Move(Vector3 _normal)
    {
        if (canMove) return;

        bool _hit = Physics.BoxCast(transform.position, blockCollider.bounds.extents, _normal, out RaycastHit _result, Quaternion.identity, length, layerMask);
        if (!_hit)
        {
            base.Move(_normal);
            destination = transform.position + _normal * length;
            canMove = true;
        }
    }


    //protected override void MoveTodestination()
    //{
    //    base.MoveTodestination();
    //    if (MathUtils.CompareVector(transform.position, destination, new Vector3(.1f, .1f, .1f)))
    //        canMove = false;
    //}

}
