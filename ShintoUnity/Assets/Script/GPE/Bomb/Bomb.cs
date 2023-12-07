using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Bomb : GPEComponent
{
    public event Action OnExplode = null;

    [SerializeField, Header("Physics")] Rigidbody body = null;
    [SerializeField] LayerMask bounceLayer;
    [SerializeField] Vector3 force = new Vector3(0, .6f, .2f);
    [SerializeField, Header("Explsion")] PaternExplosion patern = null;
    [SerializeField, Range(.1f, 100)] float explodeTime = 2;
    [SerializeField, Range(0, 1)] float holdingBombExplodePercentage = 1;

    float explodeTimer = 0;
    bool isActive = true;
    float percentageSpeed = 1;

    bool isLaunch = false;
    bool bouncing = false;


    public Quaternion PaternRotation => new Quaternion(0, transform.eulerAngles.y, 0, 0);

    public void Launch()
    {
        body.useGravity = true;
        percentageSpeed = 1;
        isLaunch = true;
    }
    public void Take()
    {
        body.useGravity = false;
        percentageSpeed = holdingBombExplodePercentage;
        isLaunch = false;
    }


    void Start()
    {
        //body.AddRelativeForce(force, ForceMode.Impulse);
    }

    void Update()
    {
        UpdateTimer();
        UpdateBounce();
    }
    void FixedUpdate()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        
    }



    void UpdateTimer()
    {
        if (!isActive) return;
        explodeTimer += Time.deltaTime * percentageSpeed;
        if (explodeTimer >= explodeTime)
        {
            isActive = false;
            Explode();
        }
    }

    private void Explode()
    {
        OnExplode?.Invoke();
        PaternExplosion _patern = Instantiate<PaternExplosion>(patern, transform.position, PaternRotation);
        Destroy(gameObject);
    }


    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere((transform.position + new Vector3(0, -.1f, 0) * .1f), .5f);
        //Gizmos.color = Color.blue;
        //Vector3 _dir = transform.forward;
        //Gizmos.DrawLine(transform.position, transform.position + _dir * 10);
    }

    void UpdateBounce()
    {
        //if (bouncing)
        //    return;
        //bool _hit = Physics.SphereCast(transform.position, .5f, new Vector3(0, -.1f, 0), out RaycastHit _result, .1f);
        //if(_hit)
        //{
            //Debug.Log("ca passe");
            //transform.rotation = Quaternion.LookRotation(_result.point - transform.position);
            //Vector3 _dir = transform.forward;
            //transform.forward = Vector3.Reflect(_dir, _result.normal);
            //body.velocity.Set(body.velocity.x, 0, body.velocity.z);
            //force = force * .5f;
            //body.AddRelativeForce(force, ForceMode.Impulse);
            //bouncing = true;
        //}
    }
}