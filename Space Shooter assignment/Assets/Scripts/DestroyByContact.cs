using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject player_explosion;
    public GameObject bolt;

    public Transform spawnPoint;
    public int scoreValue;

    public bool didPierce;
    private GameController game;
    private PowerupTimers powerup;
    // Start is called before the first frame update
    void Start()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null)
            game = GameControllerObject.GetComponent<GameController>();
        if (GameControllerObject == null)
            Debug.Log("Cannot fing 'GameController' reference");

        GameObject PowerupTimerObject = GameObject.FindWithTag("Powerup");
        if (PowerupTimerObject != null)
            powerup = PowerupTimerObject.GetComponent<PowerupTimers>();
        else
            Debug.Log("Cannot find 'PowerupTimers' reference");

        didPierce = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
            return;

        if(other.tag == "PPowerup")
        {
            Destroy(this.gameObject);
            didPierce = true;
        }
        else if (powerup.isShieldEffect == true)
        {
            if (other.tag != "Player")
                Destroy(other.gameObject);
        }
        else
            Destroy(other.gameObject);

        if (this.tag == "EnemyExplode")
            Instantiate(bolt, spawnPoint.position, spawnPoint.rotation);

        Destroy(this.gameObject);
        game.AddScore(scoreValue);

        if(explosion != null)
            Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            if (powerup.isShieldEffect == true)
            {
                Debug.Log("Shield Hit!");
                powerup.isShieldEffect = false;
            }

            else
            {
            Instantiate(player_explosion, other.transform.position, other.transform.rotation);
            game.GameOver();
            }

        }
    }
}
