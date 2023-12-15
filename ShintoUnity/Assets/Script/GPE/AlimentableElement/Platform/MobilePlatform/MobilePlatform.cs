using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : Platform
{

    [SerializeField] Waypoint pointB = null;
    [SerializeField,Range(0,10)] float speed = 5;
    [SerializeField] CustomBoxCollider boxCollider = null;
    Vector3 startPosition = Vector3.zero;

    [SerializeField]bool canMove = false;

    private void Awake()
    {
        OnActive += () => CanMove(true) ;
        OnDisable += () => CanMove(false);
        boxCollider.onTriggerEnter += InitMovement;
        boxCollider.onTriggerExit += EndMovement;
    }
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pointB)
        {
            Debug.Log("Waypoint == null");
            return;
        }
       Move(startPosition,pointB.transform.position,speed);
    }
    void InitMovement(Collider other)
    {
        Ichigo _chara = other.gameObject.GetComponent<Ichigo>();
        if (_chara)
        {
            _chara.transform.SetParent(transform);
            _chara.CanMove = false;
        }
    }
    void EndMovement(Collider other)
    {
        Ichigo _chara = other.gameObject.GetComponent<Ichigo>();
        if (_chara)
        {
            _chara.transform.SetParent(null);
            _chara.CanMove = true;
        }
    }


    void Move(Vector3 _start,Vector3 _end, float _speed)
    {

        if (canMove)
            transform.position = Vector3.Lerp(transform.position, _end, Time.deltaTime * _speed);

        else
            transform.position = Vector3.Lerp(transform.position, _start, Time.deltaTime * _speed);
    }

    void CanMove(bool _status)
    {
        canMove = _status;
    }
}
