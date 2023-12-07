using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Bomb : GPEComponent
{
    public event Action<Bomb> OnExplode = null;

    [SerializeField, Header("Physics")] Rigidbody body = null;
    [SerializeField, Header("Explsion")] PaternExplosion patern = null;
    [SerializeField, Range(.1f, 100)] float explodeTime = 2;
    [SerializeField, Range(0, 1)] float HoldingBombExplodePercentage = 1;

    float explodeTimer = 0;
    bool isActive = true;
    float percentagSpeed = 1;

    bool isLaunch = false;


    public Quaternion PaternRotation => new Quaternion(0, transform.eulerAngles.y, 0, 0);

    public void Launch()
    {
        percentagSpeed = 1;
        isLaunch = true;
    }


    void Start()
    {
        
    }
    void Update()
    {
        UpdateTimer();
    }

    void OnTriggerEnter(Collider other)
    {
        
    }



    void UpdateTimer()
    {
        if (!isActive) return;
        explodeTimer += Time.deltaTime * percentagSpeed;
        if (explodeTimer >= explodeTime)
        {
            isActive = false;
            Explode();
        }
    }

    private void Explode()
    {
        OnExplode?.Invoke(this);
        PaternExplosion _patern = Instantiate<PaternExplosion>(patern, transform.position, PaternRotation);
        Destroy(gameObject);
    }
}
