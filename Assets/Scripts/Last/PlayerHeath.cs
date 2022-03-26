using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHeath : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int health;

    Manta manman;

    float healN;

    private void Start()
    {
        health = 100;
        manman = GameObject.Find("Manta").GetComponent<Manta>();
    }

    private void Update()
    {
        healN += Time.deltaTime;

        if (healN >= 2)
        {
            Heal(1);
            healN = 0;
        }

        if (health < 0)
        {
            health = 0;
        }

        if (health > 100) health = 100;

        if(health <= 0) manman.gameOver = true; 

        healthText.text = "Health : " + health;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }

    public void Heal(int damageAmount)
    {
        health += damageAmount;
    }
}
