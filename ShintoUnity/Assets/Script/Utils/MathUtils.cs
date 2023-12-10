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

    public static bool CompareVector(Vector3 _a, Vector3 _b, Vector3 _offset)
    {
        return Mathf.Abs(_a.x - _b.x) <= _offset.x &&
             Mathf.Abs(_a.y - _b.y) <= _offset.y &&
             Mathf.Abs(_a.z - _b.z) <= _offset.z;
    }

    public static Vector3 ReplaceVectorElements(Vector3 _ogVector, Vector3 _newVector, Vector3 _normal)
    {
        return new Vector3(Mathf.Abs(_normal.x) == 1 ? _newVector.x : _ogVector.x,
                           Mathf.Abs(_normal.y) == 1 ? _newVector.y : _ogVector.y,
                           Mathf.Abs(_normal.z) == 1 ? _newVector.z : _ogVector.z);
    }
}
