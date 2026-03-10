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
        animator.Play("Idle");
    }

    private void Update()
    {
        UpdateSpriteFlip();
    }

    private void OnUnitMoveStarted()
    {
        animator.Play("Move");
    }

    private void OnUnitMoveFinished()
    {
        animator.Play("Idle");
    }

    private void UpdateSpriteFlip()
    {
        float xMove = transform.position.x - lastPosition.x;
        spriteRenderer.flipX = xMove == 0 ? spriteRenderer.flipX : xMove < 0;
        lastPosition = transform.position;
    }


}
