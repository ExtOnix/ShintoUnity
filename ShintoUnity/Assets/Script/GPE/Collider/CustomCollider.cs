using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public event Action<Collider> onTriggerEnter = null;
    public event Action<Collider> onTriggerExit = null;
    public event Action<Collider> onTriggerStay = null;


    void OnTriggerEnter(Collider other)
    {
        OnEnterBehaviour(other);
    }
    void OnTriggerExit(Collider other)
    {
        OnExitBehaviour(other);
    }
    void OnTriggerStay(Collider other)
    {
        OnStayBehaviour(other);
    }
    protected virtual void OnEnterBehaviour(Collider other)
    {
        onTriggerEnter?.Invoke(other);
    }
    protected virtual void OnExitBehaviour(Collider other)
    {
        onTriggerExit?.Invoke(other);
    }
    protected virtual void OnStayBehaviour(Collider other)
    {
        onTriggerStay?.Invoke(other);
    }


    public void DrawBoxCollider(Color _color, Matrix4x4 _matrix, Vector3 _size)
    {
        Gizmos.color = _color;
        Gizmos.matrix = _matrix;
        Gizmos.DrawWireCube(Vector3.zero, _size);
    }
    public void DrawBoxCollider(Color _color,  Vector3 _size)
    {
        Gizmos.color = _color;
        Gizmos.DrawWireCube(transform.position, _size);
    }
    public void DrawSphereCollider(Color _color, float _radius)
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
