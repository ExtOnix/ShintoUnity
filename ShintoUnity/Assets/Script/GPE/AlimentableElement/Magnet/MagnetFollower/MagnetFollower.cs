using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetFollower : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float speed = 1.0f;

    public MagnetFollower(Transform _target, float _speed)
    {
        target = _target;
        speed = _speed;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime * speed);
        if (MathUtils.CompareVector(transform.position, target.position, new Vector3(.1f, 1, .1f)))
            Destroy(gameObject);
    }
}
