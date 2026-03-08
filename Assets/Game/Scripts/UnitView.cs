using UnityEngine;

public class UnitView : MonoBehaviour
{
    private Animator animator;
    private Unit unit;
    private SpriteRenderer spriteRenderer;

    private Vector3 lastPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        unit = GetComponentInParent<Unit>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        unit.OnMoveStarted.AddListener(OnUnitMoveStarted);
        unit.OnMoveFinished.AddListener(OnUnitMoveFinished);
    }
    private void OnDisable()
    {
        unit.OnMoveStarted.RemoveListener(OnUnitMoveStarted);
        unit.OnMoveFinished.RemoveListener(OnUnitMoveFinished);
    }

    private void Start()
    {
        lastPosition = transform.position;
        animator.SetBool("Walking", false);
    }

    private void Update()
    {
        UpdateSpriteFlip();
    }

    private void OnUnitMoveStarted()
    {
        animator.SetBool("Walking", true);
    }

    private void OnUnitMoveFinished()
    {
        Debug.Log("walking finished");
        animator.SetBool("Walking", false);
    }

    private void UpdateSpriteFlip()
    {
        int xMoveDirection = Mathf.CeilToInt(transform.position.x - lastPosition.x);
        spriteRenderer.flipX = xMoveDirection == 0 ? spriteRenderer.flipX : xMoveDirection == -1;
        lastPosition = transform.position;
    }


}
