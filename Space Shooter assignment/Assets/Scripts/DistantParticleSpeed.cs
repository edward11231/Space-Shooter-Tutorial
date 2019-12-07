﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantParticleSpeed : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FarParticleSpeed(int speed)
    {
        var main = ps.main;
        main.simulationSpeed = speed;
    }
}
