using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;
    [SerializeField] private float moveSpeed;
    private bool forward;

    float speedMultiplier = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //startPoint = transform.position;
        //endPoint = transform.position + Vector3.right * 70f;
        forward = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == startPoint) forward = true;
        if (transform.position == endPoint) forward = false;

        if(forward) transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * speedMultiplier * Time.deltaTime);
        else transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * speedMultiplier *Time.deltaTime);
    }
}
