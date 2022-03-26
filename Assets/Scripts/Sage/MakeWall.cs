using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeWall : MonoBehaviour
{
    [SerializeField]
    private float maxWallDistance;
    [SerializeField]
    private Transform wallPrefab;

    [SerializeField]
    private GameObject wallObstacle;

    private bool rotateWall;
    Quaternion rot;
    Quaternion rotate;

    public LayerMask layer;

    private Camera playerCamera;

    private float cooldown;

    ManuManu man;

    public TextMeshProUGUI wallcdtext;
    public TextMeshProUGUI horverttext;
    float forwall;

    private void Awake()
    {
        man = GameObject.Find("Manta").GetComponent<ManuManu>();

        playerCamera = transform.Find("Camera").GetComponent<Camera>();
        rotateWall = false;

        cooldown = 20;

        wallObstacle.transform.position = new Vector3(0, -50, 0);
    }

    private void Update()
    {
        Handler();

        cooldown += Time.deltaTime;

        rot = transform.rotation;
        if (Input.GetKeyUp(KeyCode.Mouse1)) rotateWall = !rotateWall;
        if (Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log(man.canWall);

            RaycastHit hit;
            RaycastHit hit2;
            bool parbe = false;

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxWallDistance, layer))
            {
                if (Physics.Raycast(transform.position, -transform.up, out hit2, maxWallDistance, layer))
                {
                    if (hit.collider == hit2.collider) parbe = true;
                }
            }

            if (parbe && cooldown >= 20)
            {
                Vector3 spawnPoint = hit.point;
                if (rotateWall) rot *= Quaternion.Euler(0, 90, 0);
                Transform wall = Instantiate(wallPrefab, spawnPoint, rot);
                wallObstacle.transform.position = wall.transform.position;
                wallObstacle.transform.rotation = wall.transform.rotation;
                cooldown = 0;
            }

            if (cooldown >= 15) wallObstacle.transform.position = new Vector3(0, -50, 0);
        }
    }

    private void Handler()
    {
        forwall = 20 - cooldown;
        wallcdtext.text = ((int)forwall + 1)+ "s";
        if ((int)forwall <= 0) wallcdtext.text = "";

        if (rotateWall) horverttext.text = "Vertical";
        else horverttext.text = "Horizontal";
    }

}
