using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SinkWater : MonoBehaviour
{
    public ParticleSystem coldSinkWater;
    public ParticleSystem hotSinkWater;

    public CircularDrive coldWaterKnob;
    public CircularDrive hotWaterKnob;

    private float coldKnobAngle;
    private float hotKnobAngle;

    private AudioSource aSource;

    private bool isFlowingCold;
    private bool isFlowingHot;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        isFlowingCold = false;
        isFlowingHot = false;
    }

    void Update()
    {
        coldKnobAngle = Mathf.Abs(coldWaterKnob.outAngle);
        hotKnobAngle = Mathf.Abs(hotWaterKnob.outAngle);

        if (hotKnobAngle > 5 && isFlowingHot == false)
            HotWaterOn();

        if (hotKnobAngle < 5 && isFlowingHot)
            HotWaterOff();


        if (coldKnobAngle > 5 && isFlowingCold == false)
            ColdWaterOn();

        if (coldKnobAngle < 5 && isFlowingCold)
            ColdWaterOff();
    }

    private void HotWaterOn()
    {
        hotSinkWater.Play();
        isFlowingHot = true;
        if (aSource.isPlaying == false)
            aSource.PlayOneShot(aSource.clip); 
    }

    private void ColdWaterOn()
    {
        coldSinkWater.Play();
        isFlowingCold = true;
        if (aSource.isPlaying == false)
            aSource.PlayOneShot(aSource.clip);
    }

    private void HotWaterOff()
    {
        hotSinkWater.Stop();
        isFlowingHot = false;
        if (aSource.isPlaying)
            aSource.Stop();
    }

    private void ColdWaterOff()
    {
        coldSinkWater.Stop();
        isFlowingCold = false;
        if (aSource.isPlaying)
            aSource.Stop();
    }
}
