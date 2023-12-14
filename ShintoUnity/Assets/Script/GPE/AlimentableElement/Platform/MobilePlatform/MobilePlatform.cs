using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : Platform
{

    [SerializeField] Waypoint pointB = null;
    [SerializeField,Range(0,10)] float speed = 5;
    Vector3 startPosition = Vector3.zero;

    [SerializeField]bool canMove = false;

    private void Awake()
    {
        OnActive += () => CanMove(true) ;
        OnDisable += () => CanMove(false);
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
    private void OnTriggerEnter(Collider other)
    {
        Ichigo _chara = other.gameObject.GetComponent<Ichigo>();
        if (_chara)
        {
            _chara.transform.SetParent(transform);
            _chara.CanMove = false;
        }
    }
    private void OnTriggerExit(Collider other)
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
