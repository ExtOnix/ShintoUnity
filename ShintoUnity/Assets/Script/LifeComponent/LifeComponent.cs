using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{

    [SerializeField, Range(1, 10)] int maxLife = 5;
    int life = 1;
    bool isDead = false;
    bool inInvincibility = false;

    private void Start()
    {
        life = maxLife;
    }

    public void TakeDamages(int _damage)
    {
        if (isDead || inInvincibility)
            return;
        life -= _damage;
        Debug.Log(life);
        //onLifeChange.Broadcast(life);
        //inInvincibility = true;
        //onInvinsibilityStart.Broadcast();
        if (life == 0)
        {
            isDead = true;
            //onDie.Broadcast();
            return;
        }

    }
}
