using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnet : AlimentableElement
{
    [SerializeField] bool canAttract = false;
    [SerializeField] Waypoint pointB = null;
    [SerializeField] float speed = 2;
    [SerializeField] float length =20;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] GameObject follower = null;

    private void Awake()
    {
        OnActive += () => CanAttract(true);
        OnDisable += () => CanAttract(false);
    }
    private void Start()
    {
        UpdateMagnetRotation();
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (pointB.transform.position - transform.position).normalized * (pointB.transform.position - transform.position).magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ichigo _chara = collision.gameObject.GetComponent<Ichigo>();
        if (!_chara) return;
        ResetChara(_chara);

    }
    private void Update()
    {
        DetectPlayer();
        Attract();
    }
    void Attract()
    {
        if (!canAttract) return;
        follower.transform.position = Vector3.Lerp(follower.transform.position, new Vector3(transform.position.x, follower.transform.position.y, transform.position.z), Time.deltaTime * speed);

    }

    private void InitMagnet(Ichigo _chara)
    {
        follower.transform.position = _chara.transform.position;
        _chara.CanMove=false;
        _chara.DisableMovements();
        _chara.transform.SetParent(follower.transform);
    }

    private void ResetChara(Ichigo _chara)
    {
        _chara.CanMove = true;
        _chara.EnableMovements();
        _chara.transform.SetParent(null);
    }

    void CanAttract(bool _value)
    {
        canAttract = _value;
    }

    void UpdateMagnetRotation()
    {
        transform.eulerAngles = MathUtils.ReplaceVectorElements(transform.eulerAngles, Quaternion.LookRotation(pointB.transform.position - transform.position).eulerAngles, new Vector3(0, 1, 0));
    }

    void DetectPlayer()
    {
        if (!canAttract) return;
        bool _hitFwd = Physics.Raycast(new Ray(transform.position + transform.forward, (pointB.transform.position - transform.position).normalized), out RaycastHit _resultFwd, (pointB.transform.position - transform.position).magnitude, hitLayer);
        if (_hitFwd)
        {
            Ichigo _chara = _resultFwd.collider.GetComponent<Ichigo>();
            InitMagnet(_chara);
        }
    }
}
