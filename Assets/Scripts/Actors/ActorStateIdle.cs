using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorStateIdle : ActorState
{
    public ActorStateIdle(Character character) : base(character)
    {
        this.character = character;
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
