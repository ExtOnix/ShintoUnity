using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCannon : Cannon
{
    [SerializeField, Range(0, 360)] float angle = 0;
    [SerializeField, Range(1, 100)] float radius = 1;

    Wind currentWind = null;
    protected override void Shoot()
    {
        base.Shoot();
        currentWind.transform.position = transform.position + MathUtils.GetLocalTrigoPointXY(angle, radius, transform);

        currentWind.Direction = (currentWind.transform.position - transform.position).normalized;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canShoot) return;
        if (currentWind = other.GetComponent<Wind>())
            Shoot();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + MathUtils.GetLocalTrigoPointXY(angle, radius, transform));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + MathUtils.GetLocalTrigoPointXY(angle, radius, transform), new Vector3(.2f, .2f, .2f));
    }
}
