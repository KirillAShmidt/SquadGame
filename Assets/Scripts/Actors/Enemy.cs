using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Actor
{
    private void Start()
    {
        _currentState = _actorStateIdle;

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

            if(CheckDistance())
                StateFighting();
            else
                StateMoving();
        }
    }

    public override void FindTarget()
    {
        var characters = GridManager.Instance.characterList;

        if (characters != null)
        {
            Target = characters[UnityEngine.Random.Range(0, characters.Count)];
            Target.OnActorDestroyed += FindTarget;
        }
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
    }

    public void StateMoving()
    {
        _currentState.Exit();
        _currentState = _actorStateMoving;
        _currentState.Enter();
    }
}
