using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class LaserInput : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Action_Boolean click;
    public SteamVR_Action_Vibration haptic;
    public static GameObject currentObject;
    int currentID;

    private bool onClick;
    private Button btn;

    private Transform remoteTransform;
    private AudioSource aSource;
    private Hand rightHand;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    void Start()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        currentObject = null;
        remoteTransform = GetComponentInParent<Transform>();
        aSource = GetComponent<AudioSource>();

        if (laserPointer.pose == null)
        {
            Debug.Log(SceneHandler.rightHand);
            rightHand = SceneHandler.rightHand;
            laserPointer.pose = rightHand.GetComponent<SteamVR_Behaviour_Pose>();
        }
    }


    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.GetComponent<Button>() != null)
        {
            btn.onClick.Invoke();
            aSource.PlayOneShot(aSource.clip);
            Pulse(1, 150, 75, SteamVR_Input_Sources.RightHand);
            Debug.Log("Button was clicked");
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.GetComponent<Button>() != null)
        {
            btn = e.target.GetComponent<Button>();
            btn.Select();
            Pulse(1, 75, 75, SteamVR_Input_Sources.RightHand);
            Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.GetComponent<Button>() != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("Button was exited");
        }
    }

    public void RestorePosition()
    {
        transform.parent = remoteTransform;
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        haptic.Execute(0, duration, frequency, amplitude, source);
    }
}
