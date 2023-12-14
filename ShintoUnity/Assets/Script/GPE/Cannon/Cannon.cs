using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : GPEComponent
{
    [SerializeField] protected bool canShoot = true;
    [SerializeField, Range(1, 100)] protected float coolDown = 1;

    protected virtual void Shoot() { }
}
