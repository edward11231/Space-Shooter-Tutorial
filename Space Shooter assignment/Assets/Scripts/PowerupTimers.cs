using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTimers : MonoBehaviour
{
    public float duration = 5.0f;

    public bool isShootEffectTime;
    public bool isShieldEffect;
    public bool isTriEffectTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimerShot()
    {
        isShootEffectTime = true;
        yield return new WaitForSeconds(duration);
        isShootEffectTime = false;
    }
    IEnumerator TimerTri()
    {
        isTriEffectTime = true;
        yield return new WaitForSeconds(duration);
        isTriEffectTime = false;
    }

    public void ShootPowerUpTimer()
    {
        StartCoroutine(TimerShot());
    }

    public void ShieldPowerUpTime()
    {
        isShieldEffect = true;
    }

    public void TriShotPowerUpTime()
    {
        StartCoroutine(TimerTri());
    }
}
