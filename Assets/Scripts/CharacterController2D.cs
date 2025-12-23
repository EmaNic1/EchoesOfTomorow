using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

/// <summary>
/// judejimas ir animacija
/// </summary>

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
        // Nuskaito tiesioginę įvestį iš įvesties ašių; reikšmės bus -1, 0 arba 1.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Sukuria 2D vektorių, kurį vėliau naudosime judėjimui nustatyti.
        motionVector = new Vector2(horizontal, vertical);

        // Siunčia dabartines įvesties vertes į animatoriaus parametrus, vadinamus „horizontal“ ir „vertical“.
        // Šie parametrai dažnai naudojami maišymo medžiuose arba animacijos perėjimuose, kad būtų galima
        // vaikščioti / bėgti / stovėti priklausomai nuo krypties.
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);

        // Patikrina, ar yra įvesties duomenų (jei bent viena ašies reikšmė nėra 0).
        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving); // animatpr knows that the object ir moving

        //Jei veikėjas juda (abi ašys, ne tik 0), atnaujiname paskutinę judėjimo kryptį.
        if (horizontal != 0 || vertical != 0)
        {
            lastMotionVector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }

    /// <summary>
    /// „FixedUpdate()“ iškviečia „Move()“, „Move()“ nustato „Rigidbody2D“ greitį per „linearVelocity“
    /// </summary>
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = motionVector * speed;
    }

    void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
