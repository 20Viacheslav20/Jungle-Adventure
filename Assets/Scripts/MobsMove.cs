using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsMove : MonoBehaviour
{
    private Transform currentPoint;
    private Rigidbody2D rigidbody2d;

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentPoint = pointA.transform;
    }

    // Update is called once per frames
    void Update()
    {
        if (currentPoint == pointA.transform)
        {
            rigidbody2d.velocity = new Vector2(speed, 0);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
        
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


}