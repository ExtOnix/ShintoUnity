using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollectible : Collectible
{
    [SerializeField] Bomb bomb = null;
    private void Awake()
    {
        onTriggerEnter += GetNewBomb;
    }

    void GetNewBomb(Ichigo _chara)
    {
        _chara.AddBomb(bomb);
        Destroy(gameObject);
    }
}
