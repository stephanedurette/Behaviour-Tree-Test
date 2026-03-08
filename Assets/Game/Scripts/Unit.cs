using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnMoveStarted;
    [HideInInspector] public UnityEvent OnMoveFinished;

    private Seeker seeker;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    private void OnEnable()
    {
        seeker.pathCallback += OnMoveComplete;
    }

    private void OnDisable()
    {
        seeker.pathCallback -= OnMoveComplete;
    }

    private void OnMoveComplete(Path p)
    {

    }

    public void MoveToLocation(Vector2 destination)
    {
        seeker.StartPath(transform.position, destination);
    }
}
