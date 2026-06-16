using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 180f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Input

        movement.x =
            Input.GetAxisRaw("Horizontal");

        movement.y =
            Input.GetAxisRaw("Vertical");

        // Animator

        bool isMoving =
            movement.sqrMagnitude > 0;

        animator.SetBool(
            "IsMoving",
            isMoving);

        // Optional animator values

        animator.SetFloat(
            "MoveX",
            movement.x);

        animator.SetFloat(
            "MoveY",
            movement.y);

        // Rotation

        if (isMoving)
        {
            float angle =
                Mathf.Atan2(
                    movement.y,
                    movement.x)

                * Mathf.Rad2Deg;

            Quaternion targetRotation =

                Quaternion.Euler(
                    0f,
                    0f,
                    angle);

            transform.rotation =

                Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,

                    rotationSpeed *
                    Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(

            rb.position +

            movement.normalized *

            moveSpeed *

            Time.fixedDeltaTime
        );
    }
}