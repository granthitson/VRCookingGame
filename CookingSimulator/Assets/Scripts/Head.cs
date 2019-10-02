using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Head : MonoBehaviour
{
    public Fade fader;

    private void OnCollisionEnter(Collision collision)
    {
        fader.FadeToBlack(1f);
    }

    private void OnCollisionStay(Collision collision)
    {
        fader.FadeToBlack(0f);
    }

    private void OnCollisionExit(Collision collision)
    {
        fader.FadeFromBlack(3f);
    }
}
