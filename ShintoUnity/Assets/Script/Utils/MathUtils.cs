using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils 
{
    #region trigo
    public static Vector3 GetWorldTrigoPointXZ(float _angle, float _radius)
    {
        float _x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius,
              _z = Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        return new Vector3(_x, 0, _z);
    }
    public static Vector3 GetWorldTrigoPointXY(float _angle, float _radius)
    {
        float _x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius,
              _y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        return new Vector3(_x, _y, 0);
    }
    public static Vector3 GetWorldTrigoPointZY(float _angle, float _radius)
    {
        float _z = Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius,
              _y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        return new Vector3(0, _y, _z);
    }

    public static Vector3 GetLocalTrigoPointXZ(float _angle, float _radius, Transform _tr)
    {
        float _x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius,
              _z = Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        return _tr.forward * _x + _tr.right * _z;
    }
    public static Vector3 GetLocalTrigoPointXY(float _angle, float _radius, Transform _tr)
    {
        float _x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius,
              _y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        return _tr.forward * _x + _tr.up * _y;
    }
    public static Vector3 GetLocalTrigoPointZY(float _angle, float _radius, Transform _tr)
    {
        float _z = Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius,
              _y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        return _tr.up * _y + _tr.right * _z;
    }
    #endregion
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
