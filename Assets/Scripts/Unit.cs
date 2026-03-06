using Pathfinding;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Seeker seeker;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        seeker.StartPath(transform.position, target.position);
    }
}
