using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Block : GPEComponent
{
    [SerializeField] protected BoxCollider blockCollider = null;
    [SerializeField] protected Vector3 destination;
    [SerializeField, Range(.1f, 100)] float moveSpeed = 1;
    [SerializeField, Range(.1f, 100)] float fallSpeed = 1;
    [SerializeField] protected LayerMask moveLayer;
    [SerializeField] protected LayerMask fallLayer;

    protected bool canMove = false;
    protected bool isFalling = false;
    
    protected Vector3 direction;
    
    float currentTime = 0;

    private void Start()
    {
        destination = transform.position;
    }

    void Update()
    {
        MoveTodestination();
        UpdateTimer();
    }
    public virtual void Move(Vector3 _normal)
    {
        direction = _normal;
    }


    protected virtual void MoveTodestination()
    {
        if (!canMove) return;

        transform.position = Vector3.MoveTowards(transform.position, destination, (isFalling ? fallSpeed : moveSpeed) * Time.deltaTime);
    }


    void IsGrounded()
    {
        if (isFalling) return;

        RaycastHit[] _hits = Physics.BoxCastAll(transform.position, blockCollider.bounds.extents, Vector3.down, Quaternion.identity, 2, fallLayer);
        if (_hits.Length == 0)
        {
            isFalling = true;
            bool _hit = Physics.BoxCast(transform.position, blockCollider.bounds.extents, Vector3.down, out RaycastHit _result, Quaternion.identity, 100, moveLayer);
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


    void UpdateTimer()
    {
        if (isFalling) return;

        currentTime += Time.deltaTime;
        if (currentTime >= .1f)
        {
            currentTime = 0;
            IsGrounded();
        }
    }

}
