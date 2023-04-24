using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2d;
    private ItemCollectorScript collector;
    private Vector2 startPosition;

    private float dirX = 0f;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private AudioClip deathSoundEffect;
    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private AudioClip getDamageSoundEffect;

    private AudioSource audioSource;
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
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        collector = GetComponent<ItemCollectorScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rigidbody2d.bodyType != RigidbodyType2D.Static)
        {
            dirX = Input.GetAxis("Horizontal");
            rigidbody2d.velocity = new Vector2(dirX * speed, rigidbody2d.velocity.y);
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) && IsGrounded()) 
            {
                SetSoundEffect(jumpSoundEffect);
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            }
            UpdateAnimationState();
        }

        if (Input.GetKey(KeyCode.G))
        {
            Die();
        }

        if (Input.GetKey(KeyCode.R))
        {
            RestartLevel();
        }

        if (Input.GetKey(KeyCode.H))
        {
            DecrementLives();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillZone"))
        {
            DecrementLives();
        }
    }

    private void Die()
    {
        SetSoundEffect(deathSoundEffect);
        animator.SetTrigger("death");
        rigidbody2d.bodyType = RigidbodyType2D.Static;
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

        if (!IsGrounded() /*&& (rigidbody2d.velocity.y > .1f || rigidbody2d.velocity.y <= -.1f)*/)
        {
            state = MovementState.Jumping;
        }
        animator.SetInteger("state", (int)state);
    }

    private void Respawn()
    {
        StartCoroutine(Blink());
        transform.position = startPosition;    
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        collector.CountOfLives = 4;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, layerMask);
    }

    private IEnumerator Blink()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DecrementLives()
    {
        SetSoundEffect(getDamageSoundEffect);
        collector.CountOfLives--;
        if (collector.CountOfLives != 0)
        {
            Respawn();
        }
        else
        {
            Die();
        }
    }

    private void SetSoundEffect(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

}