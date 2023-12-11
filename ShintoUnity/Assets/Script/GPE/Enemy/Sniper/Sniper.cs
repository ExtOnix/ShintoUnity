using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Sniper : Enemy
{
    [SerializeField] SniperBullet bullet = null;
    [SerializeField] int bulletLifeTime = 2;


    [SerializeField] LayerMask shootLayer;

    [SerializeField] bool canShoot = false;
    [SerializeField] bool canDetect = false;
    private void Start()
    {
        InvokeRepeating("Shoot", 1, .5f);
    }

    private void Awake()
    {
        detection.onTriggerStay += UpdateSniperRotation;
        detection.onTriggerEnter += (c) => UpdateDetection(true);
        detection.onTriggerExit += (c) => UpdateDetection(false);
    }

    void Update()
    {
        DetectPlayer();
    }

    void Shoot()
    {
        if (!canShoot || !bullet) return;
        SniperBullet _bullet = Instantiate<SniperBullet>(bullet, transform.position + transform.forward, transform.rotation);
        _bullet.InitBullet(damages, bulletLifeTime);
    }

    void UpdateSniperRotation(Collider _chara)
    {
        transform.LookAt(_chara.transform.position);
    }

    void UpdateDetection(bool _status)
    {
        canDetect = _status;
    }

    void DetectPlayer()
    {
        if (!canDetect)
        {
            canShoot = false;
            return;
        }
        bool _hitFwd = Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit _resultFwd, detection.Size, shootLayer);
        canShoot = _hitFwd;
    }
}
