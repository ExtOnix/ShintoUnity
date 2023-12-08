using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedPlateform : Platform
{
    [SerializeField] int time = 2;
    [SerializeField] bool canFreeze = false;
    private void Awake()
    {
        OnActive += Freeze;
    }

    private void Update()
    {
        Freeze();
    }

    void Freeze()
    {
        if (!canFreeze) return;
        InvokeRepeating("Melt", time, time);
        gameObject.layer = LayerMask.NameToLayer("Wall");

    }

    void Melt()
    {
        gameObject.layer = LayerMask.NameToLayer("Transparent");
        CancelInvoke("Melt");
        canFreeze = false;
    }
}
