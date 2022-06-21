using UnityEngine;
using UnityEngine.AI;

public abstract class ActorState
{
    public Character character;

    public ActorState(Character character)
    {
        this.character = character;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
