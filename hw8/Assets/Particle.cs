using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float engineRevs;
    public float exhaustRate;

    ParticleSystem exhaust;
    // Start is called before the first frame update
    void Start()
    {
        exhaust = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        exhaust.emissionRate = engineRevs * exhaustRate;
        engineRevs = engineRevs + 10;
    }
}
