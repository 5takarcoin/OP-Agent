using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuManu : MonoBehaviour
{
    public bool canTP;
    public bool canSmoke;

    public bool canWall;
    public bool canSlow;

    public bool canUp;
    public bool canDash;

    public GameObject UItpsmoke;
    public GameObject UIwallslow;
    public GameObject UIupdash;

    public GameObject tutUItpsmoke;
    public GameObject tutUIwallslow;
    public GameObject tutUIupdash;

    public GameObject tuttut;

    Manta man;

    private void Start()
    {
        man = GameObject.Find("Manta").GetComponent<Manta>();

        None();
        TPSmoke();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            None();
            TPSmoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            None();
            WallSlow();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            None();
            UpdraftDash();
        }

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    tuttut.SetActive(!tuttut.activeSelf);

        //    //if (tuttut.activeSelf) Time.timeScale = 0;
        //    //else Time.timeScale = 1;
        //}

        if (man.gameOver) None();
    }

    public void None()
    {
        canTP = false;
        canSmoke = false;
        canWall = false;
        canSlow = false;
        canUp = false;
        canDash = false;

        UIupdash.SetActive(false);
        UIwallslow.SetActive(false);
        UItpsmoke.SetActive(false);

        tutUIupdash.SetActive(false);
        tutUIwallslow.SetActive(false);
        tutUItpsmoke.SetActive(false);
    }

    public void TPSmoke()
    {
        UItpsmoke.SetActive(true);
        tutUItpsmoke.SetActive(true);
        canTP = true;
        canSmoke = true;
    }

    public void WallSlow()
    {
        UIwallslow.SetActive(true);
        tutUIwallslow.SetActive(true); 
        canWall = true;
        canSlow = true;
    }

    public void UpdraftDash()
    {
        UIupdash.SetActive(true);
        tutUIupdash.SetActive(true);
        canUp = true;
        canDash = true;
    }
}
