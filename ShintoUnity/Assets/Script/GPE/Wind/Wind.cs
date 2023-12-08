using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : GPEComponent
{
    public event Action OnDisapear = null;

    [SerializeField] Vector3 direction;
    [SerializeField, Range(0, 100)] float speed = 10;
    [SerializeField] float offset = 1;
    [SerializeField] LayerMask wallLayer;
    [SerializeField, Range(.1f, 100)] float lifeSpan = 5;

    Collider windCollider = null;


    void Awake()
    {
        windCollider = GetComponent<Collider>();
    }
    void Start()
    {
        Invoke("Disapear", lifeSpan);
    }


    void Update() => CheckWall();
    private void LateUpdate()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    void CheckWall()
    {
        bool _hit = Physics.BoxCast(transform.position, windCollider.bounds.extents, transform.forward, out RaycastHit _result, Quaternion.identity, .1f, wallLayer);
        if (_hit)
        {
            Disapear();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        WindFollower _follower = other.gameObject.GetComponent<WindFollower>();

        if (!_follower)
            _follower = other.gameObject.AddComponent<WindFollower>();

        _follower.Init(this, offset);
    }

    void Disapear()
    {
        OnDisapear?.Invoke();
        Destroy(gameObject);
    }
}
