using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class WayPoint : MonoBehaviour
{
    public List<Enemy> enemies;
    public static WayPoint ActiveWaypoint;
    public Action OnWayPointCompleted;
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<GridManager>();

        if(player != null)
        {
            ActiveWaypoint = this;

            if (enemies.Count != 0)
            {
                GameManager.Instance.StateFighting(this);
            }
            else
            {
                GameManager.Instance.StateWalking();
            }

            SetEnemiesActive();
        }
    }

    public void DeleteEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            OnWayPointCompleted?.Invoke();
        }
    }

    public void SetEnemiesActive()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.StateMoving();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
