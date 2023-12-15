using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : CustomWidget
{

    public override void Show()
    {
        base.Show();
        Debug.Log("Show Game");
    }
}
