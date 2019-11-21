using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DishwashingLiquid : MonoBehaviour
{
    public SteamVR_Action_Boolean liquidBtn;

    private ParticleSystem soap;
    private bool liquidToggle;

    // Start is called before the first frame update
    void Start()
    {
        soap = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        liquidToggle = liquidBtn.GetState(SteamVR_Input_Sources.RightHand);
        if (liquidToggle == true)
        {
            soap.Play();
        }
        else
        {
            soap.Stop();
        }
    }
}
