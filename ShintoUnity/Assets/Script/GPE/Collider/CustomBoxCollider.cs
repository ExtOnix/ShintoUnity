using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CustomBoxCollider : CustomCollider
{
    [SerializeField] protected BoxCollider boxCollider = null;

    public Vector3 Size
    {
        get => boxCollider.size;
        set => boxCollider.size = value;
    }
};
