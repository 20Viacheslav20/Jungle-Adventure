using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageScript : MonoBehaviour
{
    private Animator animator;
    private PlayerControllerScript playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal == Vector2.down)
            {
                Rigidbody2D playerRigidBody2d = collision.gameObject.GetComponent<Rigidbody2D>();
                collision.gameObject.GetComponent<AudioSource>().Play();
                playerRigidBody2d.velocity = new Vector2(playerRigidBody2d.velocity.x, 4f);
                animator.SetTrigger("death");
                Destroy(gameObject, 0.2f);
            }
            else
            {
                playerControllerScript.DecrementLives();
            }
        }
    }
}
