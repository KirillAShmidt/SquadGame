using UnityEngine;
using System.Collections.Generic;

public abstract class GameState
{
    public GameManager gameManager;

    public GameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public abstract void Enter();
    public abstract void Exit();
}
