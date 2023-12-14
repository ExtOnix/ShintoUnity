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

    [SerializeField, Header("Physics")] Rigidbody body = null;
    [SerializeField, Header("Explosion")] PaternExplosion patern = null;
    [SerializeField, Range(.1f, 100)] float explodeTime = 2;
    [SerializeField, Range(0, 1)] float holdingBombExplodePercentage = 1;
    [SerializeField, Header("Identity")] string bombName = "Bomb";

    public string BombName => bombName;
    public float ExplosionProgress => explodeTimer / explodeTime;

    float explodeTimer = 0;
    bool isActive = true;
    float percentageSpeed = 1;


    public void Launch()
    {
        body.useGravity = true;
        percentageSpeed = 1;
    }
    public void Take()
    {
        body.useGravity = false;
        percentageSpeed = holdingBombExplodePercentage;
    }

    void Update() => UpdateTimer();

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
        PaternExplosion _patern = Instantiate<PaternExplosion>(patern, transform.position, Quaternion.identity);
        _patern.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
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
    }
}