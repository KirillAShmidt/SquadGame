using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(SelectableObject))]
public class Character : Actor
{
    public Transform GridPosition { get; set; }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void StateWalking()
    {
        _currentState.Exit();
        _currentState = _actorStateMoving;
        _currentState.Enter();
    }

    public void StateFighting()
    {
        _currentState.Exit();
        _currentState = _actorStateFighting;
        _currentState.Enter();
    }

    public override void FindTarget()
    {
        var targets = WayPoint.ActiveWaypoint.enemies;

        if (targets != null)
        {
            Target = targets[UnityEngine.Random.Range(0, targets.Count)];
            Target.OnActorDestroyed += FindTarget;
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Move() 
    {
        if (GridPosition != null)
        {
            agent.SetDestination(GridPosition.position);
        }
    }

    public override void Die()
    {
        OnActorDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _currentState.Update();
    }
}
