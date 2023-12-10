using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Block : GPEComponent
{
    [SerializeField] protected BoxCollider blockCollider = null;
    [SerializeField] protected Vector3 destination;
    [SerializeField, Range(.1f, 100)] float speed = 1;
    [SerializeField] protected LayerMask layerMask;

    [SerializeField] protected bool canMove = false;
    [SerializeField] protected bool isGrounded = true;

    [SerializeField]  Vector3 direction;


    //private void Start() => InvokeRepeating("IsGrounded", .1f, .1f);

    void Update()
    {
        MoveTodestination();
        IsGrounded();
    }
    public virtual void Move(Vector3 _normal)
    {
        direction = _normal;
    }


    protected virtual void MoveTodestination()
    {
        if (!canMove) return;

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (MathUtils.CompareVector(transform.position, destination, new Vector3(blockCollider.bounds.extents.x * 1.01f, blockCollider.bounds.extents.y * 1.01f, blockCollider.bounds.extents.z * 1.01f)))
        {
            canMove = false;
            if (isGrounded)
                StopMove();
            else RestartMovement();
        }
            
    }


    void IsGrounded()
    {
        if (!isGrounded) return;

        //bool _isGrounded = Physics.BoxCast(transform.position, blockCollider.bounds.extents, Vector3.down, out RaycastHit _floorResult, Quaternion.identity, .1f, layerMask);
        bool _isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit _floorResult, 1, layerMask);
        if (!_isGrounded)
        {
            isGrounded = _isGrounded;
            CancelInvoke("IsGrounded");
            bool _hit = Physics.BoxCast(transform.position, blockCollider.bounds.extents, Vector3.down, out RaycastHit _result, Quaternion.identity, 100, layerMask);
            if (_hit)
                destination = MathUtils.ReplaceVectorElements(transform.position, _result.point, Vector3.down);
            else destination = transform.position + Vector3.down * 100;
            canMove = true;
        }
    }

    public void StopMove()
    {
        direction = new Vector3(0, 0, 0);
    }

    void RestartMovement()
    {
        Move(direction);
        isGrounded = true;
        //InvokeRepeating("IsGrounded", .001f, .001f);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, destination);
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(destination, new Vector3(.1f, .1f, .1f));

        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position + Vector3.down * .1f, blockCollider.bounds.extents * 2);
    }

}
