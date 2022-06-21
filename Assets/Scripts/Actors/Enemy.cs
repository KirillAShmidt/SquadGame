using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Actor
{
    public float hitDistance;
    public float duration;

    private float _currentHealth;

    public EnemyState currentState;
    public EnemyStateIdle idleState;
    public EnemyStateFighting fightingState;

    public Animator _animator;
    public readonly string IDLE = "idle";
    public readonly string WALK = "walk";
    public readonly string ATTACK = "attack";

    public Action<Enemy> OnEnemyDied;

    private void Start()
    {
        idleState = new EnemyStateIdle(this);
        fightingState = new EnemyStateFighting(this);

        currentState = idleState;

        GetComponent<SelectableObject>().OnSelected += Die;
    }

    public void FixedUpdate()
    {
        currentState.Update();
    }

    public override void Attack()
    {
        base.Attack();
        _animator.SetTrigger(ATTACK);
    }

    public void MoveToTarget()
    {
        agent.SetDestination(Target.transform.position);
    }

    public void FindTarget()
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
        OnEnemyDied?.Invoke(this);
        Destroy(gameObject);
    }

    public void StateFighting()
    {
        currentState.Exit();
        currentState = fightingState;
        currentState.Enter();
    }
}
