using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float waitTime;
    
    void Start()
    {
        StartCoroutine("DesAfter");
    }

    IEnumerator DesAfter()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
