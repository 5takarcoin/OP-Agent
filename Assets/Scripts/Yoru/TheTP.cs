using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTP : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);
    }
}
