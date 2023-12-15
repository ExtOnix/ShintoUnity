using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainUI : MonoBehaviour
{
    [SerializeField] CustomDictionary<EGameState, CustomWidget> widgets;
    public void SetUI(EGameState _state)
    {
        if (!widgets.Contains(_state)) return;
        HideAlls();
        widgets[_state].Show();
    }
    void HideAlls()
    {
        for (int i = 0; i < widgets.Count(); i++)
            widgets.At(i).Hide();
    }

}
