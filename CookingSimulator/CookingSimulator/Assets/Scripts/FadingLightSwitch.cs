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

    private AudioSource aSource;
    private bool turnedOn;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        turnedOn = false;
    }

    void Update()
    {
        knobAngle = heatKnob.outAngle;

        lightAmount = Mathf.Abs((knobAngle * 1f) / 345f);
        overheadLight.intensity = lightAmount;

        if (Mathf.Abs(knobAngle) > 10)
            TurnOn();
        if (Mathf.Abs(knobAngle) < 10 && turnedOn == true)
            TurnOff();
    }

    private void TurnOn()
    {
        if (turnedOn == false)
        {
            aSource.PlayOneShot(aSource.clip);
            overheadLight.enabled = true;
            turnedOn = true;
        }
    }

    private void TurnOff()
    {
        aSource.PlayOneShot(aSource.clip);
        overheadLight.enabled = false;
        turnedOn = false;
    }
}

