using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsMove : MonoBehaviour
{
    private Transform currentPoint;
    private Rigidbody2D Rigidbody2D;
    private SpriteRenderer SpriteRenderer;

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    [SerializeField] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        currentPoint = pointA.transform;
    }

    // Update is called once per frames
    void Update()
    {
        if (currentPoint == pointA.transform)
        {
            Rigidbody2D.velocity = new Vector2(speed, 0);
        }
        else
        {
            Rigidbody2D.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
        
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    

}