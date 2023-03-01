using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private SpriteRenderer SpriteRenderer;
    private Animator Animator;
    private float dirX = 0f;

    [SerializeField] private float Speed = 6f;
    [SerializeField] private float JumpForce = 8f;

    private enum MovementState { idle, running, jumping }

    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        Rigidbody2D.velocity = new Vector2(dirX * Speed, Rigidbody2D.velocity.y);

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) 
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpForce);
            
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            Animator.SetBool("running", true);
            SpriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            Animator.SetBool("running", true);
            SpriteRenderer.flipX = true;
        }
        else
        {
            Animator.SetBool("running", false);
        }
    }

}
