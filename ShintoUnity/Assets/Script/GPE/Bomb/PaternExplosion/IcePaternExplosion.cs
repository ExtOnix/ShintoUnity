using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class IcePaternExplosion : PaternExplosion
{
    [SerializeField, Header("Ice"), Range(1, 100)] float diameter = 3;
    [SerializeField] SphereCollider paterncollider = null;
    [SerializeField, Range(.1f, 100)] float freezeTime = 2;

    void Awake()
    {
        paterncollider.radius = diameter / 2;
    }
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("SetHasDoEffectEnable", .1f, .1f);
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
        if (HasDoEffect || damagelist.Contains(other.gameObject)) 
            return;
        Bomb _bomb = other.GetComponent<Bomb>();
        if (_bomb)
            TouchBomb(_bomb);
        damagelist.Add(other.gameObject);
    }


    void TouchBomb(Bomb _bomb)
    {
        if (_bomb.BombName == "Ice")
        {
            _bomb.Explode();
            return;
        }
        _bomb.StopTime(freezeTime, null);
    }

}
