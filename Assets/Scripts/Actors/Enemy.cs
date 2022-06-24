using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Actor
{
    private void Start()
    {

        GetComponent<SelectableObject>().OnSelected += Die;
    }

    public void FixedUpdate()
    {
        _currentState.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Move()
    {
        if(Target == null)
        {
            FindTarget();
        }
        else
        {
            agent.SetDestination(Target.transform.position);

            if (CheckDistance())
                StateFighting();
            /*else
                StateMoving();*/
        }
    }

    public override void FindTarget()
    {
        var characters = GridManager.Instance.characterList;

        if (characters != null)
        {
            Target = characters[UnityEngine.Random.Range(0, characters.Count)];
            Target.OnActorDestroyed += FindTarget;
            Target.OnActorDestroyed += StateMoving;

            //StateMoving();
        }
    }

    public bool CheckDistance()
    {
        return Vector3.Distance(transform.position, Target.transform.position) < hitDistance;
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        WayPoint.ActiveWaypoint.DeleteEnemy(this);
        Destroy(gameObject);
    }

    public void StateFighting()
    {
        _currentState.Exit();
        _currentState = _actorStateFighting;
        _currentState.Enter();
        Attack();
    }

    public void StateMoving()
    {
        _currentState.Exit();
        _currentState = _actorStateMoving;
        _currentState.Enter();
    }

    public void StateIdle()
    {
        _currentState.Exit();
        _currentState = _actorStateIdle;
        _currentState.Enter();
    }
}
