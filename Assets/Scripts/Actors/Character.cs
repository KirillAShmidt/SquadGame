using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(SelectableObject))]
public class Character : Actor
{
    public float duration;
    public float hitDistance;

    private float _currentHealth;

    private ActorStateFighting actorStateFighting;
    private ActorStateIdle actorStateIdle;
    private ActorStateMoving actorStateMoving;
    private ActorState _currentState;

    public readonly string WALK = "walk";
    public readonly string FIGHT = "fight";
    public readonly string DIE = "die";


    public Transform GridPosition { get; set; }

    private void Awake()
    {
        _selectable = GetComponent<SelectableObject>();
        _selectable.OnSelected += Die;

        actorStateFighting = new ActorStateFighting(this);
        actorStateIdle = new ActorStateIdle(this);
        actorStateMoving = new ActorStateMoving(this);

        _currentState = actorStateIdle;
        _currentState.Enter();

        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void StateWalking()
    {
        _currentState.Exit();
        _currentState = actorStateMoving;
        _currentState.Enter();
    }

    public void StateFighting()
    {
        _currentState.Exit();
        _currentState = actorStateFighting;
        _currentState.Enter();
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
