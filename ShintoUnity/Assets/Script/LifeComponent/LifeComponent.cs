using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LifeComponent : MonoBehaviour
{
    public event Action OnLifeChange;
    public event Action<bool> OnDie;

    [SerializeField, Range(1, 10)] int maxLife = 5;
    int life = 1;
    bool isDead = false;
    bool inInvincibility = false;

    public int Life => life;
    public bool IsDead
    {
        get => isDead;
        set
        {
            isDead = value;
            OnDie?.Invoke(isDead);
        }
    }

    private void Start()
    {
        life = maxLife;
        OnLifeChange?.Invoke();
    }

    public void ResetLife()
    {
        life = maxLife;
        OnLifeChange?.Invoke();
    }

    public void TakeDamages(int _damage)
    {
        if (isDead || inInvincibility)
            return;
        life -= _damage;
        Debug.Log(life);
        OnLifeChange.Invoke();
        if (life == 0)
        {
            IsDead = true;
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
