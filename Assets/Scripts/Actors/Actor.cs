using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(SelectableObject), typeof(NavMeshAgent))]
public abstract class Actor : MonoBehaviour, IHaveHealth
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _damage;

    public float duration;
    public float hitDistance;
    public NavMeshAgent agent;

    protected float _currentHealth;
    protected SelectableObject _selectable;

    protected ActorStateFighting _actorStateFighting;
    protected ActorStateIdle _actorStateIdle;
    protected ActorStateMoving _actorStateMoving;
    protected ActorState _currentState;

    public readonly string IDLE = "idle";
    public readonly string WALK = "walk";
    public readonly string ATTACK = "attack";
    public readonly string DIE = "die";

    public Actor Target { get; set; }

    public Action OnActorDestroyed;

    private void Awake()
    {
        _selectable = GetComponent<SelectableObject>();
        _selectable.OnSelected += Die;

        _actorStateFighting = new ActorStateFighting(this);
        _actorStateIdle = new ActorStateIdle(this);
        _actorStateMoving = new ActorStateMoving(this);

        _currentState = _actorStateIdle;
        _currentState.Enter();

        agent = GetComponent<NavMeshAgent>();

        _currentHealth = _maxHealth;
    }

    public abstract void TakeDamage(float damage);
    public abstract void Die();
    public abstract void Move();
    public abstract void FindTarget();

    public virtual void Attack()
    {
        Debug.Log(gameObject.name + " hit " + Target.name + " with " + _damage);
        Target.TakeDamage(_damage);
    }
}
