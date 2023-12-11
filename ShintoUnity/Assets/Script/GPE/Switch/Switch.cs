using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : GPEComponent
{
    [SerializeField] protected List<AlimentableElement> elements = new();

    [SerializeField] protected bool isActive = false;



    void OnTriggerEnter(Collider other)
    {
        OnEnterBehaviour(other);
    }


    void OnTriggerExit(Collider other)
    {
        OnExitBehaviour(other);
    }

    protected virtual void OnEnterBehaviour(Collider other)
    {
        isActive = true;
    }
    protected virtual void OnExitBehaviour(Collider other)
    {
        isActive = false;
    }
}
