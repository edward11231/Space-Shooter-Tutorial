using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupEffects : MonoBehaviour
{
    public GameObject powerup;
    public Color color1;
    public Color color2;

    private Renderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        renderer = powerup.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));

    }

}
