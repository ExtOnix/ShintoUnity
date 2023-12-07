using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePaternExplosion : PaternExplosion
{

    [SerializeField, Header("Fire"), Range(.1f, 100)] float length = 3;
    [SerializeField, Range(.1f, 100)] float width = 1;
    [SerializeField, Range(.1f, 100)] float height = 1;

    [SerializeField] List<ExplosionCollider> colliders = null;


    void Awake() => InitColliders();

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("SetHasDoEffectEnable", .1f, .1f);
    }
    void InitColliders()
    {
        for (int i = 0, index = 0; i < 180; i += 180 / colliders.Count, index++)
        {
            colliders[index].Size = new Vector3(length, height, width);
            colliders[index].transform.eulerAngles += new Vector3(0, i, 0);
            colliders[index].onTriggerEnter += EnterCollider;
        }
    }
    void EnterCollider(Collider other)
    {
        if (HasDoEffect || damagelist.Contains(other.gameObject))
            return;

        Debug.Log(other.gameObject.name);
        damagelist.Add(other.gameObject);
    }
}
