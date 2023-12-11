using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CustomCollider : MonoBehaviour
{
    public event Action<Collider> onTriggerEnter = null;
    public event Action<Collider> onTriggerExit = null;

    [SerializeField] protected BoxCollider boxCollider = null;

    public Vector3 Size
    {
        get => boxCollider.size;
        set => boxCollider.size = value;
    }

    void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other);
    }
    void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke(other);
    }


    public void DrawCollider(Color _color, Matrix4x4 _matrix, Vector3 _size)
    {
        Gizmos.color = _color;
        Gizmos.matrix = _matrix;
        Gizmos.DrawWireCube(Vector3.zero, _size);
    }
    public void DrawCollider(Color _color,  Vector3 _size)
    {
        Gizmos.color = _color;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
