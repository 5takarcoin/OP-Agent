using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmokeJett : MonoBehaviour
{
    [SerializeField]
    private float distance;

    [SerializeField]
    private Transform smoke;

    private Camera playerCamera;
    private Vector3 spawnPoint;

    ManuManu man;
    float coolDown;

    public TextMeshProUGUI nadecdtext;
    float fornade;

    private void Awake()
    {
        coolDown = 15;
        playerCamera = transform.Find("Camera").GetComponent<Camera>();
        man = GameObject.Find("Manta").GetComponent<ManuManu>();
    }

    private void Update()
    {
        HandleLast();

        coolDown += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.F) && man.canSmoke && coolDown >= 8)
        {
            spawnPoint = playerCamera.transform.position + playerCamera.transform.forward * distance;
            Transform smokey = Instantiate(smoke, spawnPoint, Quaternion.identity);
            smokey.name = "Nade";
            coolDown = 0;
        }
    }

    private void HandleLast()
    {
        fornade = 8 - coolDown;
        nadecdtext.text = ((int)fornade + 1) + "s";
        if ((int)fornade <= 0) nadecdtext.text = "";
    }

}
