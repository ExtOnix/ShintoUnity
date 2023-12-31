using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Wind : GPEComponent
{
    public event Action OnDisapear = null;

    [SerializeField, Range(0, 100)] float speed = 10;
    [SerializeField] float offset = 1;
    [SerializeField] LayerMask wallLayer;
    [SerializeField, Range(.1f, 100)] float lifeSpan = 5;

    Collider windCollider = null;

    public Vector3 Direction = Vector3.forward;


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
        transform.position = transform.position + Direction * speed * Time.deltaTime;
    }

    void CheckWall()
    {
        bool _hit = Physics.BoxCast(transform.position, windCollider.bounds.extents, Direction, out RaycastHit _result, Quaternion.identity, .1f, wallLayer);
        if (_hit)
        {
            Disapear();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Ichigo>()) return;

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
