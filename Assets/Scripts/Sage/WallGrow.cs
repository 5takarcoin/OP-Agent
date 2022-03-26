using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WallGrow : MonoBehaviour
{
    [SerializeField]
    private float wallHeight;
    [SerializeField]
    private float wallBuildSpeed;
    [SerializeField]
    private float wallTime;

    Vector3 setScale;

    bool done;
    bool ekdom;

    private void Awake()
    {
        done = false;
        ekdom = false;
        transform.localScale = Vector3.zero;
        setScale = new Vector3(wallHeight, 0, wallHeight);
    }

    private void Update()
    {
       

        if (setScale.y <= wallHeight)
        {
            setScale.y += wallBuildSpeed * Time.deltaTime;
            if (setScale.y > wallHeight)
            {
                setScale = Vector3.one * wallHeight;
                done = true;
            }
            transform.localScale = setScale;
        }
        
        if(done && !ekdom)
        {
            StartCoroutine("WallDes");
            ekdom = true;
        }
    }

    IEnumerator WallDes()
    {
        yield return new WaitForSeconds(wallTime);
        Destroy(gameObject);
    }
}
