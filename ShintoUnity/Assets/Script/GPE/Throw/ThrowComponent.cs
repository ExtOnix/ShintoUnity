using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class ThrowComponent : MonoBehaviour
{
    [SerializeField, Range(.1f, 10000)] float forwardSpeed = 1;
    [SerializeField, Range(.1f, 10000)] float upSpeed = 1;

    Bomb currentBomb = null;
    public Bomb CurrentBomb
    {
        get => currentBomb;

        set
        {
            if (!value) return;
            currentBomb = value;
            currentBomb.OnExplode += BombExplode;
            currentBomb.Take();
        }
    }
    void Start()
    {
        
    }
    void BombExplode()
    {
        currentBomb = null;
    }

    public void Throw(Vector3 _fwd, Vector3 _up)
    {
        Vector3 _force = _fwd * forwardSpeed + _up * upSpeed;
        CurrentBomb.Launch();
        Rigidbody _body = CurrentBomb.GetComponent<Rigidbody>();
        _body.AddForce(_force, ForceMode.Impulse);
        currentBomb = null;
        currentBomb.OnExplode -= BombExplode;
    }
}
