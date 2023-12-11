using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Teleporter : GPEComponent
{
    [SerializeField] Waypoint teleportPoint = null;
    [SerializeField] Vector2 lookRotation = Vector2.zero;
    Timer timer = new();

    private void OnTriggerEnter(Collider other)
    {
        Ichigo _chara = other.GetComponent<Ichigo>();
        if (_chara)
            Teleport(_chara);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray _r = new Ray(teleportPoint.transform.position, new Vector3(lookRotation.x, lookRotation.y, transform.eulerAngles.z)*2);
        Gizmos.DrawRay(_r);
    }


    void Teleport(Ichigo _ichigo)
    {
        if (!teleportPoint) return;
        _ichigo.CanMove = false;
        timer.Interval = 100;
        timer.Elapsed += (e, o) =>
        {
            _ichigo.CanMove = true;
            timer.Stop();
        };
        timer.Start();
        _ichigo.gameObject.transform.position = teleportPoint.transform.position;
        //_ichigo.Arm.transform.eulerAngles = new Vector3(_ichigo.Arm.transform.eulerAngles.x,lookRotation.y, _ichigo.Arm.transform.eulerAngles.z);
    }
}
