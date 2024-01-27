using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStar : Star
{

    public float speed = 4f;

    [SerializeField()] private Transform destination;

    private int destinationIndex;
    public Transform[] travelPoints;

    Vector2 destinationVector;

    public Rigidbody2D platrb;

    private void Start()
    {
        platrb = GetComponent<Rigidbody2D>();
        transform.position = travelPoints[0].position;
        destination = travelPoints[1].transform;
        destinationIndex = 1;
    }

    public void FixedUpdate()
    {
        MoveTowardDestination();
    }

    private void MoveTowardDestination()
    {
        destinationVector = destination.position - transform.position;
        Vector3 destinationDirection = destinationVector.normalized;

        platrb.velocity = destinationDirection * speed * Time.fixedDeltaTime;

        if (destinationVector.magnitude <= 0.1f)
        {
            //Debug.Log("Arrived!");

            if (destinationIndex == travelPoints.Length - 1)
            {
                destinationIndex = 0;
            }
            else
            {
                destinationIndex++;
            }
            destination = travelPoints[destinationIndex];
        }

        Debug.DrawRay(transform.position, destinationDirection);
    }

}
