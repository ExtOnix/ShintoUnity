using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CustomSphereCollider : CustomCollider
{
    [SerializeField] protected SphereCollider sphereCollider = null;

    public float Size
    {
        get => sphereCollider.radius;
        set => sphereCollider.radius = value;
    }
}
