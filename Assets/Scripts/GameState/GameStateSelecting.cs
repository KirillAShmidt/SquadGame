using UnityEngine;

public class GameStateSelecting : GameState
{
    public GameStateSelecting(GameManager gameManager) : base(gameManager)
    {
        this.gameManager = gameManager;
    }

    public override void Enter()
    {
        Debug.Log("StateSelecting");
    }

    public override void Exit()
    {
        GInterface.Instance.SetInactive();
    }
}
