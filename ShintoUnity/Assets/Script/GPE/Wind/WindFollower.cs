using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindFollower : MonoBehaviour
{
    [SerializeField] float offset = 1;
    [SerializeField] Wind target = null;


    public Wind Target
    {
        get => target;
        set => target = value;
    }

    void LateUpdate()
    {
        transform.position = Target.transform.position + (Vector3.up * offset);
    }

    public void Init(Wind _wind, float _offset)
    {
        target = _wind;
        offset = _offset;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        _wind.OnDisapear += WindDisapear;
    }


    void WindDisapear()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        Destroy(this);
    }
}
