using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressurePlate : GPEComponent
{
    [SerializeField] CustomCollider customCollider = null;

    [SerializeField] bool isActive = false;

    [SerializeField] List<AlimentableElement> elements = new();
    [SerializeField] List<GameObject> objects = new();


    private void Awake()
    {
        customCollider.onTriggerEnter += OnPressActive;
        customCollider.onTriggerExit += OnPressDisable;
    }


    void OnPressActive(Collider _collier)
    {
        isActive = true;
        foreach (AlimentableElement _element in elements)
            _element.Active();
    }
    void OnPressDisable(Collider _collier)
    {
        isActive = false;
        foreach (AlimentableElement _element in elements)
            _element.Disable();
    }


    void OnDrawGizmos()
    {
        customCollider.DrawCollider(Color.green, customCollider.Size);
    }
}
