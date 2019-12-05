using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ItemHint : MonoBehaviour
{
    public bool remote;
    public bool knife;
    public bool soap;

    private void Start()
    {
        
    }

    private void OnHandHoverBegin(Hand hand)
    {
        if (knife)
            SceneHandler.rightHand.ShowGrabHint("Snap Grab");
        else
            hand.ShowPinchHint("Grab");
    }

    private void OnAttachedToHand(Hand hand)
    {
        if (remote)
            SceneHandler.rightHand.ShowInteractAction("Interact with UI");

        if (knife)
            SceneHandler.rightHand.ShowCutAction("Cut");

        if (soap)
            SceneHandler.rightHand.ShowInteractAction("Soap");
    }

    private void OnDetachedFromHand(Hand hand)
    {
        ControllerButtonHints.HideAllTextHints(SceneHandler.leftHand);
        ControllerButtonHints.HideAllButtonHints(SceneHandler.leftHand);

        ControllerButtonHints.HideAllTextHints(SceneHandler.rightHand);
        ControllerButtonHints.HideAllButtonHints(SceneHandler.rightHand);

        ControllerButtonHints.HideAllTextHints(hand);
        ControllerButtonHints.HideAllButtonHints(hand);
    }
}
