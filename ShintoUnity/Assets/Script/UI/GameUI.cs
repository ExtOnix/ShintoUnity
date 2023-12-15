using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : CustomWidget
{
    [SerializeField] LifeComponent characterLife = null;
    [SerializeField] Ichigo character = null;
    [SerializeField] Image heart = null;
    [SerializeField] Transform content = null;
    [SerializeField] TMP_Text bombName = null;

    private void Awake()
    {
        characterLife.OnLifeChange += GenerateLife;
        character.OnBombChange += UpdateBombName;
    }
    public override void Show()
    {
        base.Show();
    }

    void GenerateLife()
    {
        ClearLife(content);
        for (int i = 0; i < characterLife.Life; i++)
        {
            Image _heart = Instantiate(heart, content);
        }
    }

    void ClearLife(Transform _tr)
    {
        for (int i = 0; i < _tr.childCount; i++)
        {
            Destroy(_tr.GetChild(i).gameObject);
        }
    }

    void UpdateBombName()
    {
        if (!character) return;
        bombName.text = character.CurrentBomb.BombName;
    }
}
