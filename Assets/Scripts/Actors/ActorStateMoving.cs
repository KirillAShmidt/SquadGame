using UnityEngine;
using UnityEngine.AI;

public class ActorStateMoving : ActorState
{
    public ActorStateMoving(Actor actor) : base(actor)
    {
        this.actor = actor;
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        actor.Move();
    }

    public override void Exit()
    {
    }
}
