using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnMoveStarted;
    [HideInInspector] public UnityEvent OnMoveFinished;

    private Seeker seeker;
    private AILerp aiLerp;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiLerp = GetComponent<AILerp>();
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
        OnMoveFinished?.Invoke();
    }

    public void MoveToLocation(Vector2 destination, float speed)
    {
        aiLerp.speed = speed;
        seeker.StartPath(transform.position, destination);
    }
}
