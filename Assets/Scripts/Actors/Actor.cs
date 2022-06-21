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

    public Actor Target { get; set; }

    protected SelectableObject _selectable;

    public NavMeshAgent agent;

    public Action OnActorDestroyed;

    public abstract void TakeDamage(float damage);
    public virtual void Attack()
    {
        Debug.Log(gameObject.name + " hit " + Target.name);
        Target.TakeDamage(_damage);
    }
    public abstract void Die();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        _selectable = GetComponent<SelectableObject>();
    }
}
