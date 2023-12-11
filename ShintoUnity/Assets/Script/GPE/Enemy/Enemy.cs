using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(LifeComponent))]
public abstract class Enemy : GPEComponent
{
    [SerializeField] protected EnemyCustomCollider detection = null;
    [SerializeField] protected int damages = 0;

}
