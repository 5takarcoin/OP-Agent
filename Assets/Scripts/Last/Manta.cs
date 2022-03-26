using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manta : MonoBehaviour
{
    public int enemiesKilled;
    public TextMeshProUGUI text;
    public TextMeshProUGUI goText;

    public TextMeshProUGUI helpText;

    public bool gameOver;

    private void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        enemiesKilled = 0;
    }

    private void Update()
    {
        text.text = "Enemies Killed : " + enemiesKilled;

        if (gameOver)
        {
            Time.timeScale = 0;
            goText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.P)) Restart();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            helpText.gameObject.SetActive(!helpText.gameObject.activeSelf);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }
}
