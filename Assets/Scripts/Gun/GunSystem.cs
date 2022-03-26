using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public string gunType;
    public TextMeshProUGUI type;

    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int megazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //public GameObject muzzleFlash;
    public GameObject bulletHoleGraphic;
    //public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    public GameObject gunModel;

    Manta man;

    Vector3 khela;
    Vector3 dekha;

    private void Awake()
    {
        man = GameObject.Find("Manta").GetComponent<Manta>();

        bulletsLeft = megazineSize;
        readyToShoot = true;

        khela = gunModel.transform.rotation.eulerAngles;
        dekha = transform.localPosition;
    }

    private void Update()
    {
        type.text = gunType;
        //Debug.Log(transform.forward);

        if (!man.gameOver)
        {
            MyInput();


            if (bulletsLeft < 0) bulletsLeft = 0;

            if (reloading) gunModel.transform.rotation = Quaternion.Euler(khela - transform.forward * 80);
            else gunModel.transform.rotation = Quaternion.Euler(khela);

            if (reloading) text.SetText("Reloading...");

            else
                text.SetText("Ammo " + bulletsLeft + " / " + megazineSize);
        }
        else
        {
            text.SetText("");
            gunType = "";
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < megazineSize && !reloading) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        if(Physics.Raycast(attackPoint.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            
            if (rayHit.collider.CompareTag("Enemies"))
            {
                rayHit.collider.GetComponent<Enemies>().TakeDamage(damage);
            }
        }

        //camShake.Shake(camShakeDuration, camShakeMagnitude);

        Instantiate(bulletHoleGraphic, rayHit.point, transform.rotation).transform.parent = rayHit.transform;
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);



    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = megazineSize;
        reloading = false;
    }

}
