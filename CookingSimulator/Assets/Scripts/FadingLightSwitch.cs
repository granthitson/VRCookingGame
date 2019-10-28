using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FadingLightSwitch : MonoBehaviour
{
    public CircularDrive heatKnob;
    public Light overheadLight;

    private float knobAngle;
    private float lightAmount;

    void Update()
    {
        knobAngle = heatKnob.outAngle;

        lightAmount = Mathf.Abs((knobAngle * 1f) / 345f);
        overheadLight.intensity = lightAmount;

        if (Mathf.Abs(knobAngle) > 10)
            TurnOn();
        else
            TurnOff();
    }

    private void TurnOn()
    {
        overheadLight.enabled = true;
    }

    private void TurnOff()
    {
        overheadLight.enabled = false;
    }
}

