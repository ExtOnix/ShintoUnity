using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPaternExplosion : PaternExplosion
{
    [SerializeField, Header("Wind"),Range(0, 100)] float radius = 1;
    [SerializeField, Range(0, 360)] int windNumber = 4;
    [SerializeField] GameObject prefab = null;

    [SerializeField] BoxCollider boxCollider = null;

    protected override void Start()
    {
        base.Start();
        SpawnWind();
        InvokeRepeating("SetHasDoEffectEnable", .1f, .1f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (HasDoEffect || damagelist.Contains(other.gameObject))
            return;

        Rigidbody _body = other.GetComponent<Rigidbody>();
        if (_body)
            _body.AddForce(Vector3.up * 10, ForceMode.Impulse);
        damagelist.Add(other.gameObject);
    }
    void SpawnWind()
    {
        for (int i = 0; i < 360; i += 360/windNumber)
        {
            GameObject _obj = Instantiate(prefab);
            _obj.transform.position = transform.position + MathUtils.GetTrigoPoint(i, radius);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxCollider.size);
    }
}
