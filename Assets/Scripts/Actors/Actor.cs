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
    protected float _currentTime;
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

        _animator = GetComponent<Animator>();

        _currentHealth = _maxHealth;
        _currentTime = duration;
    }

    public abstract void TakeDamage(float damage);
    public abstract void Die();
    public abstract void Move();
    public abstract void FindTarget();
    public abstract void Fight();

    public virtual void Attack()
    {
        _animator.SetBool(IDLE, true);

        _currentTime -= Time.fixedDeltaTime;

        if (_currentTime <= 0)
        {
            Debug.Log(gameObject.name + " hit " + Target.name + " with " + _damage);
            Target.TakeDamage(_damage);
        }
    }

    public bool CheckDistance()
    {
        if (Target == null)
                return false;

        return Vector3.Distance(transform.position, Target.transform.position) < hitDistance;
    }
}
