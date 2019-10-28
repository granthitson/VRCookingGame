using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Fade : MonoBehaviour
{
    public void FadeToBlack(float duration)
    {
        SteamVR_Fade.Start(Color.clear, 0f);

        SteamVR_Fade.Start(Color.black, duration);
    }

    public void FadeFromBlack(float duration)
    {
        SteamVR_Fade.Start(Color.black, 0f);

        SteamVR_Fade.Start(Color.clear, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || other.tag != "PlayerHead" || other.tag != "Untagged")
        {
            FadeToBlack(.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" || other.tag != "PlayerHead" || other.tag != "Untagged")
        {
            FadeFromBlack(3f);
        }
    }
}
