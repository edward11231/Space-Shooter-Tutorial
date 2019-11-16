using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText; 

    private bool gameOver;
    private bool restart;
    private int score;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject game = GameObject.FindWithTag("Player");
        if (game != null)
            player = game.GetComponent<PlayerController>();
        else
            Debug.Log("Player GameObject not found");
        StartCoroutine (SpawnWaves());
        score = 0;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(restart)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-6, 6), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Z' to restart";
                restart = true;
                break;
            }
        }

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if(score >= 100)
        {
            winText.text = "You Win! Game created by Edward Tavarez";
            gameOver = true;
            restart = true;
            player.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
