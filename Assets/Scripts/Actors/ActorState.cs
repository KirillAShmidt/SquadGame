using UnityEngine;
using UnityEngine.AI;

public abstract class ActorState
{
    public Actor actor;

    public ActorState(Actor actor)
    {
        this.actor = actor;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
