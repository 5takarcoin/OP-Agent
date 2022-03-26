using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGuns : MonoBehaviour
{
    public GameObject models;

    int current;
    int len;

    private void Start()
    {
        current = 0;
        len = transform.childCount;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) current = (current + 1) % len;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) current = (current + len - 1) % len;

        for (int i = 0; i < len; i++)
        {
            if (i == current)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                models.transform.GetChild(i).gameObject.SetActive(true);
            }

            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
                models.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        rifle.SetActive(true);
    //        machineGun.SetActive(false);
    //        shotgun.SetActive(false);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        rifle.SetActive(false);
    //        machineGun.SetActive(true);
    //        shotgun.SetActive(false);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        rifle.SetActive(false);
    //        machineGun.SetActive(false);
    //        shotgun.SetActive(true);
    //    }
    //}


}
