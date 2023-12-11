using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : GPEComponent
{
    [SerializeField] IceBlock prefab = null;
    [SerializeField] BoxCollider boxCollider = null;


    void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<IcePaternExplosion>()) return;
        IceBlock _block = Instantiate<IceBlock>(prefab, transform.position, transform.rotation);
        _block.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
}
