using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private List<WayPoint> _wayPoints;
    private List<WayPoint> _currentWaypoints;

    public WayPoint Target { get; private set; }

    public static NavigationManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _wayPoints = new List<WayPoint>();
        _wayPoints.AddRange(FindObjectsOfType<WayPoint>());

        int n = _wayPoints.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (_wayPoints[j].index > _wayPoints[j + 1].index)
                {
                    WayPoint temp = _wayPoints[j];
                    _wayPoints[j] = _wayPoints[j + 1];
                    _wayPoints[j + 1] = temp;
                }
            }
        }

        if (_wayPoints != null)
            Target = _wayPoints[0];
    }

    public void MoveToNextTarget()
    {
        _agent.SetDestination(NextTarget().transform.position);
    }

    public WayPoint NextTarget()
    {
        _wayPoints.Remove(_wayPoints[0]);
        return _wayPoints[0];
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
