using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCustomCollider : CustomSphereCollider
{ 

    protected override void OnEnterBehaviour(Collider other)
    {
        if (other.GetComponent<Ichigo>())
            base.OnEnterBehaviour(other);
    }
    protected override void OnExitBehaviour(Collider other)
    {
        if (other.GetComponent<Ichigo>())
            base.OnExitBehaviour(other);
    }
    protected override void OnStayBehaviour(Collider other)
    {
        if (other.GetComponent<Ichigo>())
            base.OnStayBehaviour(other);
    }
}
