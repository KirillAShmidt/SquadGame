using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorStateFighting : ActorState
{
    public List<Enemy> enemies;

    public ActorStateFighting(Actor actor) : base(actor)
    {
        this.actor = actor;
    }

    public override void Enter()
    {
        enemies = WayPoint.ActiveWaypoint.enemies;

        animator = actor.GetComponent<Animator>();
        animator.SetTrigger(actor.IDLE);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
    }
}
