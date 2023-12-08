using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
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

        Bomb _bomb = other.GetComponent<Bomb>();
        if (_bomb)
            TouchBomb(_bomb);

        LifeComponent _life = other.GetComponent<LifeComponent>();
        if (_life)
            _life.TakeDamages(other.GetComponent<Ichigo>() ? playerDamage : damage);
        damagelist.Add(other.gameObject);
    }


    void TouchBomb(Bomb _bomb)
    {
        if (_bomb.BombName == "Fire")
        {
            _bomb.Explode();
            return;
        }
        Destroy(_bomb.gameObject);
    }

    void OnDrawGizmos()
    {
        Matrix4x4 _matrix = new();
        Quaternion _rotation = new();
        for (int i = 0, index = 0; i < 180; i += 180 / colliders.Count, index++)
        {
            _rotation = transform.rotation;
            _rotation.eulerAngles += new Vector3(0, i, 0);
            _matrix = Matrix4x4.TRS(transform.position, _rotation, colliders[index].transform.lossyScale);
            colliders[index].DrawCollider(Color.red, _matrix, new Vector3(length, height, width));
        }
    }
}
