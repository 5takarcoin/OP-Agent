using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SageSlow : MonoBehaviour
{
    public GameObject slow;

    public float distance;

    Vector3 spawnPoint;
    public GameObject playerCamera;

    ManuManu man;

    float coolDown;

    public TextMeshProUGUI slowCDtext;

    float forslow;

    private void Start()
    {
        coolDown = 15;
        man = GameObject.Find("Manta").GetComponent<ManuManu>();
    }

    private void Update()
    {
        HandleO();

        coolDown += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Q) && man.canSlow && coolDown >= 10)
        {
            spawnPoint = playerCamera.transform.position + playerCamera.transform.forward * distance;
            Instantiate(slow, spawnPoint, Quaternion.identity);
            coolDown = 0;
        }
    }

    private void HandleO()
    {
        forslow = 10 - coolDown;
        slowCDtext.text = ((int)forslow + 1) + "s";
        if ((int)forslow <= 0) slowCDtext.text = "";
    }
}
