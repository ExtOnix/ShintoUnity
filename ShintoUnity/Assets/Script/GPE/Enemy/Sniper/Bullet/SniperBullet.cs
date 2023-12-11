using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    int damage = 0;
    int time = 3;

    public void  InitBullet(int _damage, int _time)
    {
        damage = _damage;
        time = _time;
        Destroy(gameObject, time);
    }

    private void OnTriggerEnter(Collider other)
    {
        Ichigo _chara = other.GetComponent<Ichigo>();
        if (_chara)
            MakeDamage(_chara);
        Destroy(gameObject);
    }

    void MakeDamage(Ichigo _ichigo)
    {
        LifeComponent _life = _ichigo.GetComponent<LifeComponent>();
        if (_life)
            _life.TakeDamages(damage);
    }
    private void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.position += transform.forward * 5f * Time.deltaTime;
    }
}
