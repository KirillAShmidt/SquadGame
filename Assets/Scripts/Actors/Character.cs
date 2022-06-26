using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(SelectableObject))]
public class Character : Actor
{
    public Transform GridPosition { get; set; }

    public void ChangeState(ActorState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
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

    public override void Fight()
    {
        _animator.SetBool(WALK, false);
        _animator.SetBool(IDLE, true);

        if (CheckDistance())
            Attack();
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
            _animator.SetBool(IDLE, false);
            _animator.SetBool(WALK, true);

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
        currentState.Update();
    }
}
