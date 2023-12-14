using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CustomWaypointCollider : MonoBehaviour
{
    public event Action<Ichigo> onTriggerEnter = null;
    private void OnTriggerEnter(Collider other)
    {
        Ichigo _chara = other.GetComponent<Ichigo>();
        if (_chara)
            onTriggerEnter?.Invoke(_chara);
    }

}
