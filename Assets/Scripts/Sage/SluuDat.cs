using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SluuDat : MonoBehaviour
{
    Enemies ag;
    public LayerMask enemy;

    private void Update()
    {
        Collider[] cld = Physics.OverlapBox(transform.position, transform.localScale * 0.5f, transform.rotation, enemy);
        int len = cld.Length;
        if (len > 0){
            for (int i = 0; i < len; i++)
            {
                ag = cld[i].GetComponent<Enemies>();
                ag.SlowDem();
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Enemies"))
    //    {
    //        Debug.Log("Hosse");
    //        ag = collision.collider.GetComponent<NavMeshAgent>();
    //        ag.speed *= 0.2f;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Enemies"))
    //    {
    //        Debug.Log("HosseNa");
    //        ag = collision.collider.GetComponent<NavMeshAgent>();
    //        ag.speed *= 5f;
    //    }
    //}
}
