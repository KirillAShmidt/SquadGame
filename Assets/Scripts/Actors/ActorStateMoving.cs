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
        if (actor != null)
        {
            animator = actor.GetComponent<Animator>();
            animator.SetBool(actor.WALK, true);
        }

        //actor.Move();
    }

    public override void Update()
    {
        actor.Move();
    }

    public override void Exit()
    {
        if(actor != null)
            animator.SetBool(actor.WALK, false);
    }
}
