using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject player_explosion;
    public int scoreValue;

    private GameController game;
    // Start is called before the first frame update
    void Start()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null)
            game = GameControllerObject.GetComponent<GameController>();
        if (GameControllerObject == null)
            Debug.Log("Cannot fing 'GameController' reference");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
            return;

        Destroy(other.gameObject);
        Destroy(this.gameObject);
        game.AddScore(scoreValue);

        if(explosion != null)
            Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(player_explosion, other.transform.position, other.transform.rotation);
            game.GameOver();
        }
    }
}
