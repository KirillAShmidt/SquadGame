using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWalking : GameState
{
    public GameStateWalking(GameManager gameManager) : base(gameManager)
    {
        this.gameManager = gameManager;
    }

    public override void Enter()
    {
        Debug.Log("StateWalking");
        NavigationManager.Instance.MoveToNextTarget();

        foreach (Character character in gameManager.characterList)
        {
            character.ChangeState(character.actorStateMoving);
        }
    }

    public override void Exit()
    {
    }
}
