using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFighting : GameState
{
    private WayPoint _wayPoint;

    public GameStateFighting(GameManager gameManager, WayPoint wayPoint) : base(gameManager)
    {
        this.gameManager = gameManager;
        this._wayPoint = wayPoint;
    }

    public override void Enter()
    {
        Debug.Log("StateFighting");
        _wayPoint.OnWayPointCompleted += CompleteWayPoint;
        
        foreach(var unit in GridManager.Instance.characterList)
        {
            unit.ChangeState(unit.actorStateFighting);
        }
    }

    private void CompleteWayPoint() 
    {
        gameManager.StateWalking();
    }

    public override void Exit()
    {
    }
}
