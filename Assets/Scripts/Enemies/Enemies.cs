using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    [SerializeField] public int health;
    NavMeshAgent yo;
    float store;

    public Manta manu;

    public PlayerHeath pG;

    //private void Start()
    //{
    //    health = 100;
    //}

    private void Start()
    {
        yo = GetComponent<NavMeshAgent>();
        store = yo.speed;
        manu = GameObject.Find("Manta").GetComponent<Manta>();

        pG = GameObject.Find("Player").GetComponent<PlayerHeath>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            manu.EnemyKilled();
            pG.Heal(20);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void SlowDem()
    {
        yo.speed = store * 0.1f;
        StartCoroutine("Slow");
    }

    IEnumerator Slow()
    {
        yield return new WaitForSeconds(5);
        yo.speed = store;
    }

    public void Smoked()
    {
        health -= 200;
    }
}
