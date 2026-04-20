using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    private const float MinLookDirectionSqrMagnitude = 0.0001f;

    [SerializeField] private Transform _pointsRoot;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _arrivalDistance = 0.05f;

    private Transform[] _points;
    private int _currentPointIndex;

    private void Start()
    {
        if (_pointsRoot == null)
        {
            Debug.LogError($"{nameof(WaypointMover)}: Points root is not assigned.", this);
            enabled = false;
            return;
        }

        if (_pointsRoot.childCount == 0)
        {
            Debug.LogError($"{nameof(WaypointMover)}: Points root has no child points.", this);
            enabled = false;
            return;
        }

        _points = new Transform[_pointsRoot.childCount];

        for (int i = 0; i < _pointsRoot.childCount; i++)
            _points[i] = _pointsRoot.GetChild(i);
    }

    private void Update()
    {
        Transform targetPoint = _points[_currentPointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            _moveSpeed * Time.deltaTime);

        Vector3 direction = targetPoint.position - transform.position;

        if (direction.sqrMagnitude > MinLookDirectionSqrMagnitude) 
            transform.forward = direction.normalized;

        if (( transform.position - targetPoint.position ).sqrMagnitude <= _arrivalDistance * _arrivalDistance)
            SelectNextPoint();
    }

    private void SelectNextPoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _points.Length)
            _currentPointIndex = 0;
    }
}