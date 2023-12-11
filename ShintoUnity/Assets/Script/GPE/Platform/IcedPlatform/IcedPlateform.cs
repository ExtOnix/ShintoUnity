using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedPlateform : Platform
{
    [SerializeField] int time = 2;
    [SerializeField] Material initMaterial = null;
    [SerializeField] Material freezeMaterial = null;
    private void Awake()
    {
        OnActive += Freeze;
    }
    void Freeze()
    {
        InvokeRepeating("Melt", time, time);
        gameObject.layer = LayerMask.NameToLayer("Wall");
        gameObject.GetComponent<Renderer>().material = freezeMaterial;


    }

    void Melt()
    {
        gameObject.layer = LayerMask.NameToLayer("Transparent");
        gameObject.GetComponent<Renderer>().material = initMaterial;
        CancelInvoke("Melt");
    }
}
