using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorStateFighting : ActorState
{
    public ActorStateFighting(Actor actor) : base(actor)
    {
        this.actor = actor;
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (actor.Target == null && WayPoint.ActiveWaypoint)
        {
            actor.FindTarget();
        }
        else
        {
            actor.Fight();
        }
    }

    public override void Exit()
    {
    }
}
