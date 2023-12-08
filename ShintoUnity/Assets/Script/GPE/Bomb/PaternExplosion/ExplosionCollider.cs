using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExplosionCollider : MonoBehaviour
{
    public event Action<Collider> onTriggerEnter = null;

    [SerializeField] BoxCollider boxCollider = null;

    public Vector3 Size
    {
        get => boxCollider.size;
        set => boxCollider.size = value;
    }

    void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other);
    }


    public void DrawCollider(Color _color, Matrix4x4 _matrix, Vector3 _size)
    {
        Gizmos.color = _color;
        Gizmos.matrix = _matrix;
        Gizmos.DrawWireCube(Vector3.zero, _size);
    }
}
