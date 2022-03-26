using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public float timeToSpawn;

    GameObject holder;

    private void Start()
    {
        holder = GameObject.Find("Enemy Holder");
        StartCoroutine("SpawnEnemies");
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeToSpawn);
        if(holder.transform.childCount <= 10) Instantiate(enemy, transform.position, Quaternion.Euler(Vector3.zero)).transform.parent = holder.transform;
        StartCoroutine("SpawnEnemies");
    }
}
