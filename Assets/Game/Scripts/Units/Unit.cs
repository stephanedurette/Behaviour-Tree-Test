using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnMoveStarted;
    [HideInInspector] public UnityEvent OnMoveFinished;

    private Seeker seeker;
    private AILerp aiLerp;

    protected virtual void Awake()
    {
        seeker = GetComponent<Seeker>();
        aiLerp = GetComponent<AILerp>();
    }

    public void MoveToLocation(Vector2 destination, float speed)
    {
        aiLerp.speed = speed;
        seeker.StartPath(transform.position, destination);
        StartCoroutine(WaitForTargetReachedCoroutine());
    }

    private IEnumerator WaitForTargetReachedCoroutine()
    {
        OnMoveStarted?.Invoke();
        yield return new WaitUntil(() => !aiLerp.reachedEndOfPath);
        yield return new WaitUntil(() => aiLerp.reachedEndOfPath);
        OnMoveFinished?.Invoke();
    }
}
