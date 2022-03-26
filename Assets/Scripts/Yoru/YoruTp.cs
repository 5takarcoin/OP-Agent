using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YoruTp : MonoBehaviour
{
    public GameObject yoru;
    public GameObject yo;

    public float speed;

    bool ready;
    bool go;
    float yola;

    ManuManu man;

    float coolDown;

    PlayerCharacterController pcc;

    public TextMeshProUGUI tpcdtext;
    public TextMeshProUGUI tpstatetext;
    float forytp;

    void Start()
    {
        pcc = GetComponent<PlayerCharacterController>();
        man = GameObject.Find("Manta").GetComponent<ManuManu>();

        coolDown = 20;
    }

    void Update()
    {
        HandleBro();

        coolDown += Time.deltaTime;


        yoru = GameObject.Find("Yoru");


        if (pcc.can && Input.GetKeyDown(KeyCode.Mouse1)) go = !go;

        if (Input.GetKeyDown(KeyCode.Q) && man.canTP)
        {

            if (yoru)
            {
                Vector3 here = yoru.transform.position;
                Destroy(yoru);
                Debug.Log(here);
                transform.position = here;
                Debug.Log(transform.position);
            }

            else
            {
                GameObject no;
                if (coolDown >= 10)
                {
                    no = Instantiate(yo, transform.position + transform.forward * 5, Quaternion.identity);
                    coolDown = 0;
                    no.name = "Yoru";

                    if (!go) yola = 0;
                    else yola = 1;
                    no.GetComponent<TheTP>().dir = transform.forward * speed * yola;
                }
               
            }
        }
    }

    private void HandleBro()
    {
        forytp = 10 - coolDown;
        tpcdtext.text = ((int) forytp + 1) + "s";
        if ((int)forytp <= 0) tpcdtext.text = "";

        if (yoru) tpstatetext.text = "Teleport";
        else
        {
            if (go) tpstatetext.text = "Send";
            else tpstatetext.text = "Place";
        }

    }
}
