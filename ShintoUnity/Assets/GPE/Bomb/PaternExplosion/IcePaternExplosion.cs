using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class IcePaternExplosion : PaternExplosion
{
    [SerializeField, Header("Ice"), Range(1, 100)] float diameter = 3;
    [SerializeField] SphereCollider paterncollider = null;

    void Awake()
    {
        paterncollider.radius = diameter / 2;
    }
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, diameter / 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (damagesObjects.ContainsKey(other.gameObject.name))
            return;
        Debug.Log(other.gameObject.name);
        damagesObjects.Add(other.gameObject.name, other.gameObject);
    }

}
