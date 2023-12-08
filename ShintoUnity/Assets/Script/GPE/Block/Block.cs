using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : GPEComponent
{
    [SerializeField] protected BoxCollider blockCollider = null;
    [SerializeField] protected Vector3 destination;
    [SerializeField, Range(.1f, 100)] float speed = 1;
    [SerializeField] protected LayerMask layerMask;

    [SerializeField] protected bool canMove = false;

    void Update()
    {
        MoveTodestination();
    }
    public virtual void Move(Vector3 _normal) { }


    protected virtual void MoveTodestination()
    {
        if (!canMove) return;

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, destination);
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(destination, new Vector3(.1f, .1f, .1f));
    }

}
