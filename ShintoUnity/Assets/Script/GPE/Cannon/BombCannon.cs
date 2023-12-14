using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ThrowComponent))]
public class BombCannon : Cannon
{
    [SerializeField] Bomb bomb = null;
    [SerializeField] ThrowComponent throwSystem = null;
    [SerializeField, Header("Mode")] bool shootOnPlayer = true;
    [SerializeField] EnemyCustomCollider detectZone = null;



    Ichigo player = null;



    void Start()
    { 
        throwSystem = GetComponent<ThrowComponent>();
        detectZone.onTriggerEnter += EnterZone;
        detectZone.onTriggerExit += ExitZone;
    }


    void Update()
    {
        LookAtPlayer();
    }


    protected override void Shoot()
    {
        base.Shoot();
        Bomb _bomb = Instantiate<Bomb>(bomb, transform.position + transform.forward, Quaternion.identity);
        throwSystem.CurrentBomb = _bomb;
        throwSystem.Throw(transform.forward, Vector3.zero);
    }





    void LookAtPlayer()
    {
        if (!player || !shootOnPlayer) return;
        Quaternion _quat = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _quat.eulerAngles.y, transform.eulerAngles.z);
    }



    void EnterZone(Collider _collider)
    {
        player = _collider.GetComponent<Ichigo>();
        if (player)
            InvokeRepeating("Shoot", coolDown, coolDown);
    }

    void ExitZone(Collider _collider)
    {
        if (_collider.GetComponent<Ichigo>())
        {
            player = null;
            CancelInvoke("Shoot");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 3);

        detectZone.DrawSphereCollider(Color.yellow, detectZone.Size);
    }

}
