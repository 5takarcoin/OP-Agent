using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGoBrr : MonoBehaviour
{
    private SphereCollider sCol;
    private Rigidbody rb;
    [SerializeField] 
    private float maxD;
    [SerializeField]
    private float speed;

    [SerializeField]
    private float smokeTime;

    GameObject player;
    Transform playerCam;

    bool smokee;

    private bool smokeBool = false;

    public LayerMask enemy;

    Enemies enm;

    public LayerMask layers;

    float timo;

    private void Awake()
    {
        timo = 0;
        smokee = false;
        sCol = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        sCol.radius = 1;
        player = GameObject.Find("Player");
        playerCam = player.transform.Find("Camera").transform;
    }

    private void Update()
    {
        timo += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.F)) smokee = true;
        else if (Vector3.Distance(playerCam.transform.position, transform.position) >= maxD)
        {
            smokee = true;
        }
        else if (timo >= 10)
        {
            smokee = true;
        }
        else if (!smokeBool)
        {
            transform.position += playerCam.transform.forward * speed * Time.deltaTime;
        }

        if (smokee)
        {
            CheckYo();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        smokee = true;
    }


    private void smoked()
    {
        transform.localScale = Vector3.one * 10;
        sCol.radius = 0.5f;
        smokeBool = true;
        //Destroy(sCol);
        Destroy(rb);
        StartCoroutine("SmokeDes");
    }


    IEnumerator SmokeDes()
    {
        yield return new WaitForSeconds(smokeTime);
        Destroy(gameObject);
    }

    public void CheckYo()
    {
        Collider[] haha = Physics.OverlapSphere(transform.position, 10, enemy);
        int len = haha.Length;

        if(len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                enm = haha[i].GetComponent<Enemies>();
                enm.Smoked();
            }
        }
    }
}
