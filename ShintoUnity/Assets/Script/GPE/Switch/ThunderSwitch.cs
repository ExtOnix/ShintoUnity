using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSwitch : Switch
{
    List<PaternExplosion> thunderPatern = new();


    protected override void OnEnterBehaviour(Collider other)
    {
        ExplosionCollider _collider = other.GetComponent<ExplosionCollider>();
        if (!_collider) return;
        if (_collider.Patern.PaternName != "Thunder") return;

        base.OnEnterBehaviour(other);
        thunderPatern.Add(_collider.Patern);
        foreach (AlimentableElement _element in elements)
            _element.Active();
    }
    protected override void OnExitBehaviour(Collider other)
    {
        ExplosionCollider _collider = other.GetComponent<ExplosionCollider>();
        if (!_collider) return;
        if (_collider.Patern.PaternName != "Thunder") return;

        thunderPatern.Remove(_collider.Patern);
        if (thunderPatern.Count != 0) return;
        base.OnExitBehaviour(other);
        foreach (AlimentableElement _element in elements)
            _element.Disable();
    }
}
