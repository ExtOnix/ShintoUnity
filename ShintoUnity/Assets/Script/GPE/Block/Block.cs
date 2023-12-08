using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : GPEComponent
{
    [SerializeField] protected BoxCollider blockCollider = null;
    [SerializeField] protected Vector3 destination;
    [SerializeField, Range(.1f, 100)] float speed = 1;
    [SerializeField] protected LayerMask layerMask;

    protected bool canMove = false;

    void Update()
    {
        MoveTodestination();
    }


    public virtual void Move(RaycastHit _hitInfo)
    {
        
    }


    void MoveTodestination()
    {
        if (!canMove) return;

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (MathUtils.CompareVector(transform.position, destination, new Vector3(blockCollider.size.x * 1.1f, blockCollider.size.y * 1.1f, blockCollider.size.z * 1.1f)))
            canMove = false;
    }
}
