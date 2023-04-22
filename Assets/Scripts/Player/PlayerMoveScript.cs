using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2d;
    private ItemCollector collector;

    private float dirX = 0f;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private LayerMask layerMask;

    private enum MovementState 
    { 
        Idle = 0, 
        Running = 1, 
        Jumping = 2
    }

    private MovementState state = MovementState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        collector = GetComponent<ItemCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("state", (int)state);
        dirX = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(dirX * speed, rigidbody2d.velocity.y);
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) && IsGrounded()) 
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            spriteRenderer.flipX = false;
            state = MovementState.Running;
        }
        else if (dirX < 0f)
        {
            spriteRenderer.flipX = true;
            state = MovementState.Running;
        }
        else
        {         
            state = MovementState.Idle;    
        }

        if (!IsGrounded() && (rigidbody2d.velocity.y > .1f || rigidbody2d.velocity.y < -.1f))
        {
            state = MovementState.Jumping;
        }

        animator.SetInteger("state", (int) state);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, layerMask);
    }

    public float knockbackForce = 1000f;
    //void OnCollisionEnter2D(Collision2D collision)
    //{

    //    //if (collision.gameObject.tag == "Enemy")
    //    //{
    //    //    if (collision.contacts[0].normal == Vector2.up)
    //    //    {
    //    //        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 4f);
    //    //        Destroy(collision.gameObject);
    //    //    }
    //    //    else
    //    //    {
    //    //        collector.CountOfLives--;
    //    //    }
    //    //}
    //}
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Получаем контакт столкновения
    //       ContactPoint2D contact = collision.contacts[0];
    //        Debug.Log("Contact point: " + contact.point + ", Contact normal: " + contact.normal);

    //        Определяем направление отбрасывания
    //       Vector2 knockbackDirection = contact.normal;
    //        Debug.Log("Knockback direction: " + knockbackDirection);

    //        Применяем отбрасывание
    //        Rigidbody2D rb = GetComponent<Rigidbody2D>();

    //        rb.velocity = knockbackDirection * knockbackForce;
    //        rb.velocity = new Vector2(knockbackForce, rigidbody2d.velocity.y);
    //    }
    //}

}
