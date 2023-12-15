using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    None,
    Menu,
    Game,
    Pause
}


public class GameMode : Singleton<GameMode>
{
    [SerializeField] MainUI mainUI = null;
    [SerializeField] EGameState gameState = EGameState.None;

    public EGameState GameState
    {
        get => gameState;
        set
        {
            gameState = value;
            mainUI.SetUI(gameState);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        mainUI.SetUI(GameState);
    }

}
