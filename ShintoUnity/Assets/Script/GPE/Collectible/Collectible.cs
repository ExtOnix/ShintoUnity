using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public event Action<Ichigo> onTriggerEnter = null;
    private void OnTriggerEnter(Collider other)
    {
        Ichigo _chara = other.GetComponent<Ichigo>();
        if (_chara)
            onTriggerEnter?.Invoke(_chara);
    }
}
