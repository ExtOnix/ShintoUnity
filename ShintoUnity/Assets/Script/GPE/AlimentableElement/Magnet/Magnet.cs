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
    [SerializeField] LayerMask hitLayer;
    [SerializeField] MagnetFollower follower  = null;

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
        Bomb _bomb = collision.gameObject.GetComponentInChildren<Bomb>();
        if (_chara)
            ResetChara(_chara);
        else if(_bomb)
            ResetBombs(_bomb);

    }
    private void Update()
    {
        DetectPlayer();
    }

    private void InitMagnetWithChara(Ichigo _chara)
    {
        if (_chara.GetComponentInParent<MagnetFollower>()) return;
        MagnetFollower _follower = Instantiate(follower);
        _follower.transform.position = _chara.transform.position;
        _chara.CanMove=false;
        _chara.DisableMovements();
        _chara.transform.SetParent(_follower.transform);
    }
    void InitMagnet(Bomb _bomb)
    {
        if (_bomb.GetComponentInParent<MagnetFollower>()) return;
        MagnetFollower _follower = Instantiate(follower);
        _follower.transform.position = _bomb.transform.position;
        _bomb.transform.SetParent(_follower.transform);
        _bomb.Take();

    }

    private void ResetChara(Ichigo _chara)
    {
        _chara.CanMove = true;
        _chara.EnableMovements();
        _chara.transform.SetParent(null);
    }

    void ResetBombs(Bomb _bomb)
    {
        _bomb.transform.SetParent(null);
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
            Bomb _bomb = _resultFwd.collider.GetComponentInChildren<Bomb>();
            if (_chara)
            {
                InitMagnetWithChara(_chara);
                return;
            }
            else if (_bomb)
            {
                InitMagnet(_bomb);
                return;
            }
        }
    }
}
