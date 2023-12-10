using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceBlock : Block
{

    public override void Move(Vector3 _normal)
    {
        if (canMove) return;
        base.Move(_normal);

        bool _hit = Physics.BoxCast(transform.position, blockCollider.bounds.extents, _normal, out RaycastHit _result, Quaternion.identity, 100, moveLayer);
        if (_hit)
            destination = MathUtils.ReplaceVectorElements(transform.position, _result.point, _normal);
        else destination = transform.position + _normal * 100;
        canMove = true;
    }

    protected override void MoveTodestination()
    {
        base.MoveTodestination();
        if (MathUtils.CompareVector(transform.position, destination, new Vector3(blockCollider.bounds.extents.x * 1.01f, blockCollider.bounds.extents.y * 1.01f, blockCollider.bounds.extents.z * 1.01f)))
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
    //    if (MathUtils.CompareVector(transform.position, destination, new Vector3(blockCollider.bounds.extents.x * 1.01f, blockCollider.bounds.extents.y * 1.01f, blockCollider.bounds.extents.z * 1.01f)))
    //        Invoke(isGrounded ? "StopMove" : "RestartMovement", 0);
    //}
}
