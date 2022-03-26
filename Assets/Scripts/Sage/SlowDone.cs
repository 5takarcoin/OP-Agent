using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDone : MonoBehaviour
{
    public GameObject spawnThis;
    GameObject player;
    public float slowSpeed;

    Vector3 direction;
    Vector3 temp;
    Rigidbody rb;

    bool done;

    public LayerMask layer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        temp = transform.position;
        rb.AddForce((Camera.main.transform.forward) * slowSpeed, ForceMode.Impulse);
        done = false;
    }

    public void Update()
    {
        direction = transform.position - temp;
        temp = transform.position;

        if (!done && Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit2, layer))
        {
            if(hit2.distance <= transform.localScale.x)
            {
                Instantiate(spawnThis, hit2.point + Vector3.up * spawnThis.transform.localScale.y * 0.5f, Quaternion.identity);
                Destroy(gameObject);
                done = true;
            }           
        }

    }
}
