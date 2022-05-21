using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 targetPosition;

    public float moveSpeed;
    private bool movingToTarget;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        movingToTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToTarget == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                movingToTarget = false;
            }
        }
        else
        {
            Destroy(gameObject);

        }
    }

  
}
