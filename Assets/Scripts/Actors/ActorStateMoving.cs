using UnityEngine;
using UnityEngine.AI;

public class ActorStateMoving : ActorState
{
    public ActorStateMoving(Character character) : base(character)
    {
        this.character = character;
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        if (character.GridPosition != null)
        {
            character.agent.SetDestination(character.GridPosition.position);
        }
    }

    public override void Exit()
    {
    }
}
