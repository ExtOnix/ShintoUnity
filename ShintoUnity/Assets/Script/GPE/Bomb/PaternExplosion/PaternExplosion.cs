using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PaternExplosion : GPEComponent
{
    [SerializeField, Header("Patern")] string paternName = "Patern";
    [SerializeField] protected int damage = 1;
    [SerializeField] protected int playerDamage = 1;
    [SerializeField, Range(.1f, 100)] float duration = 3;
    [SerializeField] protected bool HasDoEffect = false;

    protected List<GameObject> damagelist = new();
    public string PaternName => paternName;

    protected virtual void Start() => InvokeRepeating("DestroyPatern", duration, duration);

    void DestroyPatern()
    {
        transform.position = new Vector3(0, -1000, 0);
        Destroy(gameObject, .1f);
    }

    protected void SetHasDoEffectEnable()
    {
        HasDoEffect = true;
    }
}
