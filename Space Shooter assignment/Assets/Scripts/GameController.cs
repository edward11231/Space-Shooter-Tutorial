using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] powerups;
    
    public Vector3 spawnValue;
    public int hazardCount;
    public int powerupDropRate;
    public int powerupRange;
    public int hazardRange;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float waveCount;

    public float eventGoal;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public AudioClip victoryMusic;
    public AudioClip defeatMusic;
    public AudioSource AudioSource;

    private bool gameOver;
    private bool restart;
    private int score;

    private int eventScore;
    private float waveCounter;
    private float pathCounter;
    private PlayerController player;
    private PathDifficultySettings path;
    // Start is called before the first frame update
    void Start()
    {
        GameObject game = GameObject.FindWithTag("Player");
        if (game != null)
            player = game.GetComponent<PlayerController>();
        else
            Debug.Log("Player GameObject not found");

        GameObject pds = GameObject.FindWithTag("Settings");
        if (pds != null)
            path = pds.GetComponent<PathDifficultySettings>();
        else
            Debug.Log("Object with PathDifficultySetting Script not found");
        StartCoroutine (SpawnWaves());
        score = 0;
        eventScore = 0;
        powerupRange = powerups.Length;
        hazardRange = 4;
        waveCounter = 0;
        pathCounter = 0;
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

    public IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (waveCount > waveCounter)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazardRange)];
                GameObject powerup = powerups[Random.Range(0, powerupRange)];
                Vector3 spawnPosition = new Vector3(Random.Range(-6, 6), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (Random.Range(0, powerupDropRate) == 1)
                    Instantiate(powerup, spawnPosition, spawnRotation);
                else
                    Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

            }
            yield return new WaitForSeconds(waveWait);
            waveCounter++;

            if (gameOver)
            {
                restartText.text = "Press 'Z' to restart";
                restart = true;
                break;
            }

        }
        if(gameOver == false)
        {
            path.easyPath.SetActive(true);
            path.hardPath.SetActive(true);
            waveCounter = 0;
            pathCounter++;
            if (pathCounter >= 3)
                GameOver();
        }
        path.leftPath = false;
        path.rightPath = false;

    }

    public void AddScore(int newScoreValue)
    {
        if(path.isDoubleScore == true)
        {
            score += (newScoreValue * 2);
            eventScore += (newScoreValue * 2);
            UpdateScore();
        }
        else
        {
            score += newScoreValue;
            eventScore += newScoreValue;
            UpdateScore();
        }

    }
    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        /*if(score >= 100 && gameOver == false)
        {
            winText.text = "You Win! Game created by Edward Tavarez";
            gameOver = true;
            restart = true;
            player.gameObject.SetActive(false);
        }*/
    }

    public void GameOver()
    {
        if(pathCounter >= 3)
        {
            gameOverText.text = "Expedition Complete! Your Score was: " + score + " Game created by Edward Tavarez";
            restartText.text = "Press 'Z' to restart";
            AudioSource.clip = victoryMusic;
            AudioSource.Play();
            AudioSource.loop = true;
            player.gameObject.SetActive(false);
            path.easyPath.SetActive(false);
            path.hardPath.SetActive(false);
            restart = true;
        }
        else
        {
            gameOverText.text = "Game Over. Your score was: " + score + " Game created by Edward Tavarez";
            AudioSource.clip = defeatMusic;
            AudioSource.Play();
            AudioSource.loop = true;
        }
         gameOver = true;       
    }
}
