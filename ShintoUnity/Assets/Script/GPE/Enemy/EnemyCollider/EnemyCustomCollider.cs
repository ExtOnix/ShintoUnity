using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyCustomCollider : MonoBehaviour
{
    public event Action<Collider> onTriggerEnter = null;
    public event Action<Collider> onTriggerExit = null;
    public event Action<Collider> onTriggerStay = null;

    [SerializeField] protected SphereCollider sphereCollider = null;

    public float Size
    {
        get => sphereCollider.radius;
        set => sphereCollider.radius = value;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ichigo>())
            onTriggerEnter?.Invoke(other);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ichigo>())
            onTriggerExit?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Ichigo>())
            onTriggerStay.Invoke(other);
    }

    public void DrawCollider(Color _color, float _radius)
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
