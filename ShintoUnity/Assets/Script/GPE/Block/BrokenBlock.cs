using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBlock : GPEComponent
{
    [SerializeField] CustomBoxCollider boxCollider = null;


    void Start()
    {
        boxCollider.onTriggerEnter += OnEnter;
    }


    void OnEnter(Collider other)
    {
        ExplosionCollider _collider = other.GetComponent<ExplosionCollider>();
        if (!_collider) return;
        if (_collider.Patern.PaternName == "Fire")
            Destroy(gameObject);
    }
}
