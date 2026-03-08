using Pathfinding;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Seeker seeker;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    public void MoveToLocation(Vector2 destination)
    {
        seeker.StartPath(transform.position, destination);
    }
}
