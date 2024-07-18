using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypointIndex;
    private float _defaultYPosition;

    private void Awake()
    {
        _defaultYPosition = transform.localScale.y;
    }

    private void Update()
    {
        MoveThroughWaypoints();
    }

    private void MoveThroughWaypoints()
    {
        Vector3 _currentWaypoint = new(_waypoints[_currentWaypointIndex].position.x, _defaultYPosition, _waypoints[_currentWaypointIndex].position.z);
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint, _moveSpeed * Time.deltaTime);

        if (transform.position == _currentWaypoint)
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
        }
    }
}