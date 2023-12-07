using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils 
{
    public static Vector3 GetTrigoPoint(float _angle, float _radius)
    {
        float _x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius,
              _z = Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        return new Vector3(_x, 0, _z);
    }
}
