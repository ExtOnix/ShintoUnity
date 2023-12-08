using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Bomb : GPEComponent
{
    public event Action OnExplode = null;
    public event Action OnTimeStopFinish = null;

    [SerializeField, Header("Physics")] Rigidbody body = null;
    [SerializeField] LayerMask bounceLayer;
    [SerializeField] Vector3 force = new Vector3(0, .6f, .2f);
    [SerializeField, Header("Explosion")] PaternExplosion patern = null;
    [SerializeField, Range(.1f, 100)] float explodeTime = 2;
    [SerializeField, Range(0, 1)] float holdingBombExplodePercentage = 1;
    [SerializeField, Header("Identity")] string bombName = "Bomb";

    public string BombName => bombName;
    public float ExplosionProgress => explodeTimer / explodeTime;

    float explodeTimer = 0;
    bool isActive = true;
    float percentageSpeed = 1;

    bool isLaunch = false;
    //bool bouncing = false;


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

    public void Explode()
    {
        OnExplode?.Invoke();
        PaternExplosion _patern = Instantiate<PaternExplosion>(patern, transform.position, PaternRotation);
        Destroy(gameObject);
    }

    public void StopTime(float _time, Action _callBack)
    {
        isActive = false;
        Timer _timer = new Timer();
        _timer.Interval = _time * 1000f;
        _timer.Elapsed += (s, e) =>
        {
            isActive = true;
            _callBack?.Invoke();
            _timer = null;
        };
        _timer.Start();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.green, Color.red, ExplosionProgress);
        Gizmos.DrawSphere(transform.position + Vector3.up, .5f * (1 - ExplosionProgress));



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