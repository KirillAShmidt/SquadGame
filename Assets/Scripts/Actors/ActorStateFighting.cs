using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorStateFighting : ActorState
{
    public List<Enemy> enemies;

    private float _currentTime;

    public ActorStateFighting(Actor actor) : base(actor)
    {
        this.actor = actor;
    }

    public override void Enter()
    {
        _currentTime = actor.duration;
        enemies = WayPoint.ActiveWaypoint.enemies;
    }

    public override void Update()
    {
        if (actor.Target == null)
            actor.FindTarget();

        if (actor.Target != null)
        {
            if (CheckDistance())
            {
                Tick();
            }
        }
    }

    public override void Exit()
    {
    }

    private void Tick()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            _currentTime = actor.duration;
            actor.Attack();
        }
    }

    private bool CheckDistance()
    {
        return Vector3.Distance(actor.transform.position, actor.Target.transform.position) < actor.hitDistance;
    }
}
