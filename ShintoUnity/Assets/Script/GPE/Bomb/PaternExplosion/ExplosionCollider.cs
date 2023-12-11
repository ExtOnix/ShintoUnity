using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExplosionCollider : CustomCollider
{
    [SerializeField] PaternExplosion patern = null;
    public PaternExplosion Patern => patern;
}
