using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class settingSinkWater : MonoBehaviour
{
    public CircularDrive waterKnob;
    public ParticleSystem sinkWater;

    private float knobAngle;


    void Update()
    {
        knobAngle = waterKnob.outAngle;
        if (knobAngle > 1)
        {
            WaterOn();
        }
        if (knobAngle < 1)
        {
            WaterOff();
        } 
    }

    private void WaterOn()
    {
        sinkWater.Play();
    }

    private void WaterOff()
    {
        sinkWater.Stop();
    }
}
