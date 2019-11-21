using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class InputHandler : MonoBehaviour
{
    public SteamVR_ActionSet defaultSet;
    public SteamVR_ActionSet remoteSet;
    public SteamVR_ActionSet knifeSet;

    public SteamVR_Action_Boolean UIToggle;
    public SteamVR_Action_Boolean UIToggleKnife;
    public SteamVR_Action_Boolean UIToggleRemote;

    public GameObject UICanvas;

    private bool UIState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        toggleUI();
    }

    private void toggleUI()
    {
        UIState = UIToggle.GetState(SteamVR_Input_Sources.LeftHand);

        if (UIState == false)
            UICanvas.SetActive(false);
        else
            UICanvas.SetActive(true);
    }
}
