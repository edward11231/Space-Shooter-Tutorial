using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float tilt;
    public float fireRate = 0.5f;

    public Boundary boundary;
    public GameObject shot;
    public GameObject pierceShot;
    public GameObject triShot;
    public GameObject shield;
    public GameObject piercePowerup;
    public GameObject shieldPowerup;
    public GameObject triPowerup;    
    public Transform shotSpawn;
    private AudioSource source;

    private Rigidbody rb;
    private PathDifficultySettings path;
    private PowerupTimers power;
    private float nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        shield.SetActive(false);

        GameObject game = GameObject.FindWithTag("Settings");
        if(game!= null)
        {
            path = game.GetComponent<PathDifficultySettings>();
        }
        else
            Debug.Log("Object with PathDifficultySetting Script not found");

        GameObject difficulty = GameObject.FindWithTag("Powerup");
        if (difficulty != null)
        {
            power = difficulty.GetComponent<PowerupTimers>();
        }
        else
            Debug.Log("PowerupSettings Object not found");

    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (power.isShootEffectTime == true)
                Instantiate(pierceShot, shotSpawn.position, shotSpawn.rotation);
            else if (power.isTriEffectTime == true)
                Instantiate(triShot, shotSpawn.position, shotSpawn.rotation);
            else
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            source.Play();

        }
        if (power.isShieldEffect == true)
            shield.SetActive(true);
        if (power.isShieldEffect == false)
            shield.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.yMin, boundary.yMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paths")
        {
            if (other.gameObject.name == "EasyPath")
            {
                Debug.Log("Easy Path Selected");
                path.EasyPathChanges();
            }
            else if (other.gameObject.name == "HardPath")
            {
                Debug.Log("Hard Path Selected");
                path.HardPathChanges();
            }
        }

        if (other.tag == "PPowerup")
        {
            Debug.Log("Shot Upgraded Selected");
            power.ShootPowerUpTimer();
            Destroy(other.gameObject);
        }
        else if (other.tag == "SPowerup")
        {
            Debug.Log("Shield Selected");
            power.ShieldPowerUpTime();
            Destroy(other.gameObject);
        }
        else if (other.tag == "TPowerup")
        {
            Debug.Log("Tri Shot Selected");
            power.TriShotPowerUpTime();
            Destroy(other.gameObject);
        }

    }
    


}
