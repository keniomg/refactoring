using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _waypointsSet;

    private Transform[] _waypoints;
    private int _currentWaypointIndex;

    private void Start()
    {
        if (_waypointsSet.childCount != 0)
        {
            _waypoints = new Transform[_waypointsSet.childCount];

            for (int i = 0; i < _waypointsSet.childCount; i++)
            {
                _waypoints[i] = _waypointsSet.GetChild(i).GetComponent<Transform>();
            }
        }
    }

    private void Update()
    {
        MoveThroughWaypoints();
    }

    private void MoveThroughWaypoints()
    {
        Transform currentWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, _moveSpeed * Time.deltaTime);

        if (transform.position == currentWaypoint.position)
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;

            Vector3 currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
            Vector3 moveDirection = currentWaypointPosition - transform.position;
            transform.forward = moveDirection;
        }
    }
}