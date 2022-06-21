using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorStateFighting : ActorState
{
    public List<Enemy> enemies;

    private float _currentTime;

    public ActorStateFighting(Character character) : base(character)
    {
        this.character = character;
    }

    public override void Enter()
    {
        _currentTime = character.duration;
        enemies = WayPoint.ActiveWaypoint.enemies;
    }

    public override void Update()
    {
        if (character.Target == null && GridManager.Instance.characterList.Count > 0)
            FindTarget();

        if (character.Target != null)
        {
            MoveToTarget();

            if (CheckDistance())
            {
                Tick();
            }
        }
    }

    public override void Exit()
    {
    }

    private void FindTarget()
    {
        var targets = enemies;

        if (targets != null)
        {
            character.Target = targets[UnityEngine.Random.Range(0, targets.Count)];
            character.Target.OnActorDestroyed += FindTarget;
        }
    }

    private void Tick()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            _currentTime = character.duration;
            character.Attack();
        }
    }

    public void MoveToTarget()
    {
        character.agent.SetDestination(character.Target.transform.position);
    }

    private bool CheckDistance()
    {
        return Vector3.Distance(character.transform.position, character.Target.transform.position) < character.hitDistance;
    }
}
