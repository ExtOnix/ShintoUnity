using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : Block
{
    [SerializeField, Header("Push")] float length = 1;

    public override void Move(Vector3 _normal)
    {
        if (canMove) return;

        bool _hit = Physics.BoxCast(transform.position, blockCollider.bounds.extents, _normal, out RaycastHit _result, Quaternion.identity, length, moveLayer);
        if (!_hit)
        {
            base.Move(_normal);
            destination = transform.position + _normal * length;
            canMove = true;
        }
    }


    protected override void MoveTodestination()
    {
        base.MoveTodestination();
        if ( isFalling)
        {
            if (MathUtils.CompareVector(transform.position, destination, new Vector3(blockCollider.bounds.extents.x * 1.01f, blockCollider.bounds.extents.y * 1.01f, blockCollider.bounds.extents.z * 1.01f)))
            {
                canMove = false;
                if (isFalling)
                    Move(direction);
                else StopMove();
                isFalling = false;
            }
            return;
        }
        if (MathUtils.CompareVector(transform.position, destination, new Vector3(.001f,.001f,.001f)))
        {
            canMove = false;
            if (isFalling)
                Move(direction);
            else StopMove();
            isFalling = false;
        }
    }


    //protected override void MoveTodestination()
    //{
    //    base.MoveTodestination();
    //    if (MathUtils.CompareVector(transform.position, destination, new Vector3(.1f, .1f, .1f)))
    //        canMove = false;
    //}

}
