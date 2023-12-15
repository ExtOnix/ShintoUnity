using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LifeComponent : MonoBehaviour
{
    public event Action OnLifeChange;

    [SerializeField, Range(1, 10)] int maxLife = 5;
    int life = 1;
    bool isDead = false;
    bool inInvincibility = false;

    public int Life => life;

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
        OnLifeChange.Invoke();
        //onInvinsibilityStart.Broadcast();
        if (life == 0)
        {
            isDead = true;
            //onDie.Broadcast();
            return;
        }
        inInvincibility = true;
        Invoke("InvincibilityOff", 2);
    }

    void InvincibilityOff()
    {
        inInvincibility = false;
    }
}
