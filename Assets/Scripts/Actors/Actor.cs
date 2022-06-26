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

    protected Animator _animator;

    public float duration;
    public float hitDistance;
    public NavMeshAgent agent;

    protected float _currentHealth;
    protected float _lastAttackTime = -99999f;
    protected SelectableObject _selectable;

    public ActorStateFighting actorStateFighting;
    public ActorStateIdle actorStateIdle;
    public ActorStateMoving actorStateMoving;
    public ActorState currentState;

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

        actorStateFighting = new ActorStateFighting(this);
        actorStateIdle = new ActorStateIdle(this);
        actorStateMoving = new ActorStateMoving(this);

        currentState = actorStateIdle;
        currentState.Enter();

        agent = GetComponent<NavMeshAgent>();

        _animator = GetComponent<Animator>();

        _currentHealth = _maxHealth;
    }

    public abstract void TakeDamage(float damage);
    public abstract void Die();
    public abstract void Move();
    public abstract void FindTarget();
    public abstract void Fight();

    public virtual void Attack()
    {
        _animator.SetBool(IDLE, true);

        if(Time.time > _lastAttackTime + duration)
        {
            _animator.SetTrigger(ATTACK);

            _lastAttackTime = Time.time;
        }
    }

    public void AttackLand()
    {
        Debug.Log(gameObject.name + " hit " + Target.name + " with " + _damage);
        Target.TakeDamage(_damage);
    }

    public bool CheckDistance()
    {
        if (Target == null)
                return false;

        return Vector3.Distance(transform.position, Target.transform.position) < hitDistance;
    }
}
