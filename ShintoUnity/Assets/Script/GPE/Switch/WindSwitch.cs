using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class WindSwitch : Switch
{
    protected override void OnEnterBehaviour(Collider other)
    {
        Wind _wind = other.GetComponent<Wind>();
        if (!_wind) return;
        isActive = isActive ? Disable() : Active();
    }


    protected override void OnExitBehaviour(Collider other)
    {

    }

    bool Active()
    {
        //foreach (AlimentableElement _element in elements)
        //    _element.Active();
        return true;
    }
    bool Disable()
    {
        //foreach (AlimentableElement _element in elements)
        //    _element.Disable();
        return false;
    }

}
