using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDifficultySettings : MonoBehaviour
{
    public GameObject easyPath;
    public GameObject hardPath;
    public GameObject background;
    public GameObject background2;

    public Color easy;
    public Color hard;

    public float speed = 1.0f;
    public bool rightPath = false;
    public bool leftPath = false;
    public bool isDoubleScore = false;
    private Renderer renderer1;
    private Renderer renderer2;
    private GameController gameControl;
    private ParticleSpeed ps;
    private DistantParticleSpeed dps;

    private bool isDiffPick = false;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        easyPath.SetActive(false);
        hardPath.SetActive(false);
        startTime = Time.time;
        renderer1 = background.GetComponent<Renderer>();
        renderer2 = background2.GetComponent<Renderer>();

        GameObject game = GameObject.FindWithTag("GameController");
        if (game != null)
            gameControl = game.GetComponent<GameController>();
        else
            Debug.Log("Game Controller Object is missing");

        GameObject particle = GameObject.FindWithTag("Particles");
        if (particle != null)
            ps = particle.GetComponent<ParticleSpeed>();
        else
            Debug.Log("Particle Speed Object is missing");

        GameObject backParticle = GameObject.FindWithTag("BackParticles");
        if (backParticle != null)
            dps = backParticle.GetComponent<DistantParticleSpeed>();
        else
            Debug.Log("Distance Particle Speed Object is missing");
    }

    // Update is called once per frame
    void Update()
    {
        if(isDiffPick)
        {
            if(leftPath)
            {
                EasyColorChange();
                easyPath.SetActive(false);
                hardPath.SetActive(false);
            }
                
            if (rightPath)
            {
                HardColorChange();
                easyPath.SetActive(false);
                hardPath.SetActive(false);
            }


        }
    }

    public void EasyPathChanges()
    {
        isDiffPick = true;
        leftPath = true;
        isDoubleScore = false;
        ps.CloseParticleSpeed(6);
        dps.FarParticleSpeed(6);
        gameControl.powerupRange = gameControl.powerups.Length;
        gameControl.hazardRange = 4;
        gameControl.eventGoal += 300;
        gameControl.hazardCount = 15;
        gameControl.spawnWait = 0.3f;
        gameControl.waveWait = 3;
        StartCoroutine(gameControl.SpawnWaves());
    }
    public void HardPathChanges()
    {
        isDiffPick = true;
        rightPath = true;
        isDoubleScore = true;
        ps.CloseParticleSpeed(10);
        dps.FarParticleSpeed(10);
        gameControl.powerupRange = gameControl.powerups.Length - 1;
        gameControl.hazardRange = gameControl.hazards.Length;
        gameControl.eventGoal += 900;
        gameControl.hazardCount = 20;
        gameControl.spawnWait = 0.2f;
        StartCoroutine(gameControl.SpawnWaves());
    }
    private void EasyColorChange()
    {
       float t = (Time.time - startTime) * speed;
       renderer1.material.color = Color.Lerp(renderer1.material.color, easy, t);
       renderer2.material.color = Color.Lerp(renderer2.material.color, easy, t);
    }
    private void HardColorChange()
    {
        float t = (Time.time - startTime) * speed;
        renderer1.material.color = Color.Lerp(renderer1.material.color, hard, t);
        renderer2.material.color = Color.Lerp(renderer2.material.color, hard, t);
    }
}
