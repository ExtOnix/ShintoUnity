using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressurePlate : GPEComponent
{
    [SerializeField] CustomCollider customCollider = null;

    [SerializeField] List<AlimentableElement> elements = new();
    [SerializeField] List<GameObject> objects = new();


    private void Awake()
    {
        customCollider.onTriggerEnter += OnPressActive;
        customCollider.onTriggerExit += OnPressDisable;
    }


    void OnPressActive(Collider _collier)
    {
        foreach (AlimentableElement _element in elements)
            _element.Active();
    }
    void OnPressDisable(Collider _collier)
    {
        foreach (AlimentableElement _element in elements)
            _element.Disable();
    }



}
