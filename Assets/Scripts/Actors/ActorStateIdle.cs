using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorStateIdle : ActorState
{
    public ActorStateIdle(Actor actor) : base(actor)
    {
        this.actor = actor;
    }

    public override void Enter()
    {
        animator = actor.GetComponent<Animator>();
        animator.SetBool(actor.IDLE, true);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        if(animator != null)
            animator.SetBool(actor.IDLE, false);
    }
}
