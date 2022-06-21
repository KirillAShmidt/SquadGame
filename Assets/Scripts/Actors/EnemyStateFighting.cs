using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFighting : EnemyState
{
    private float _currentTime;

    public EnemyStateFighting(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        _currentTime = enemy.duration;
    }

    public override void Update()
    {
        if (enemy.Target == null && GridManager.Instance.characterList.Count > 0)
            enemy.FindTarget();

        if (enemy.Target != null)
        {
            enemy.MoveToTarget();

            if (CheckDistance())
            {
                Tick();
            }
        }
    }

    public override void Exit()
    {

    }

    private void Tick()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            _currentTime = enemy.duration;
            enemy.Attack();
        }
    }

    private bool CheckDistance()
    {
        return Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) < enemy.hitDistance;
    }
}
