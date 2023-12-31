using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class AlimentableElement : MonoBehaviour
{ 
    public event Action OnActive;
    public event Action OnDisable;
    
    virtual protected void Reset()
    {
    }

    virtual public void Active()
    {
        OnActive.Invoke();
    }

    virtual public void Disable()
    {
        OnDisable.Invoke();

    }
}
