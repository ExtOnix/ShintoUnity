using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PaternExplosion : GPEComponent
{
    [SerializeField, Header("Patern")] protected int damage = 1;
    [SerializeField] protected int playerDamage = 1;
    [SerializeField, Range(.1f, 100)] float duration = 3;
    [SerializeField] protected bool HasDoEffect = false;

    protected List<GameObject> damagelist = new();

    protected virtual void Start()
    {
        InvokeRepeating("DestroyPatern", duration, duration);
        Debug.Log("start");
    }

    void Update()
    {
    }


    void DestroyPatern()
    {
        Destroy(gameObject);
    }

    protected void SetHasDoEffectEnable()
    {
        HasDoEffect = true;
    }
}
