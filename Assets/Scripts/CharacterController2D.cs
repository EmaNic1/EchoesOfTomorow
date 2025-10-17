using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 2f;
    public Vector2 motionVector;
    public Vector2 lastMotionVector;
    Animator animator;
    public bool moving;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Reads direct input from the Input axes; values ​​will be -1, 0, or 1.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Creates a 2D vector that we will later use to determine movement.
        motionVector = new Vector2(horizontal, vertical);

        // Sends the current input values ​​to the Animator parameters named "horizontal" and "vertical".
        // These parameters are often used in blend trees or animation transitions to allow
        // walking/running/standing depending on direction.
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);

        // Checks if there is any input (if at least one axis value is not 0).
        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving); // animatpr knows that the object ir moving

        // If the character is moving (both axes not just 0), we update the last direction of movement.
        if (horizontal != 0 || vertical != 0)
        {
            lastMotionVector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }

    /// <summary>
    /// FixedUpdate() calls Move(), Move() sets the velocity of the Rigidbody2D via linearVelocity
    /// </summary>

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = motionVector * speed;
    }
}
