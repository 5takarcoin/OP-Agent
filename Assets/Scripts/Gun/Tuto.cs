using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField]
    public GameObject tut;

    [SerializeField]
    public GameObject obj;

    [SerializeField]
    public GameObject tuttu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tut.SetActive(!tut.activeSelf);
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            obj.SetActive(!obj.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            tuttu.SetActive(!tuttu.activeSelf);
        }

        if (obj.activeSelf || tut.activeSelf || tuttu.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
