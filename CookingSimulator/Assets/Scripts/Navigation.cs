using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Navigation : MonoBehaviour
{
    public float sensitivity = 0.55f;
    public float deadZone = 0.15f;
    public float rotateInc = 0.75f;

    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 trackPadMove;
    public SteamVR_Action_Boolean trackPadRunning;
    public SteamVR_Action_Boolean movementToggle;
    public SteamVR_Action_Vector2 snap;

    public SteamVR_Action_Boolean leftDPAD;
    public SteamVR_Action_Boolean rightDPAD;

    public GameObject Teleportation;
    private Teleport playerTeleportation;

    private bool movementSwitch;
    private bool run = false;
    private Vector2 axis;

    public Transform player;
    public Transform playerCollider;
    public Transform cameraRig;

    private Vector3 cameraOrientationEuler;
    private Quaternion cameraOrientation;

    private void Start()
    {
        actionSet.Activate();

        playerTeleportation = Teleportation.GetComponent<Teleport>();
        ToggleTeleporting();
    }

    // Update is called once per frame
    void Update()
    {
        axis = trackPadMove.GetAxis(SteamVR_Input_Sources.LeftHand);

        PlayerPositionRotation();
        SnapRotate();
        ToggleTeleporting();

        if (playerTeleportation.enabled == false)
        {
            ToggleRunning();
            CalculateMovement();
        }
    }

    private void PlayerPositionRotation()
    {
        Vector3 oldPosition = cameraRig.position;
        float height = cameraRig.position.y/2;

        cameraOrientationEuler = new Vector3(0, cameraRig.transform.eulerAngles.y, 0);
        cameraOrientation = Quaternion.Euler(cameraOrientationEuler);

        playerCollider.position = new Vector3(oldPosition.x, height, cameraRig.position.z);
        playerCollider.transform.eulerAngles = cameraOrientationEuler;

        Vector3 scale = new Vector3(playerCollider.transform.localScale.x, height, playerCollider.transform.localScale.z);
        playerCollider.localScale = scale;
    }

    private void SnapRotate()
    {
        float snapValue = 0.0f;

        if (leftDPAD.GetState(SteamVR_Input_Sources.RightHand))
        {
            snapValue = -Mathf.Abs(rotateInc);
        }

        if (rightDPAD.GetState(SteamVR_Input_Sources.RightHand))
        {
            snapValue = Mathf.Abs(rotateInc);
        }

        transform.RotateAround(cameraRig.position, Vector3.up, snapValue);
    }

    private void CalculateMovement()
    {
        cameraOrientationEuler = new Vector3(0, cameraRig.transform.eulerAngles.y, 0);
        cameraOrientation = Quaternion.Euler(cameraOrientationEuler);

        Vector3 movement = Vector3.zero;
        if (trackPadMove.axis == Vector2.zero || (Mathf.Abs(trackPadMove.axis.x) < deadZone && Mathf.Abs(trackPadMove.axis.y) < deadZone))
        {
            //Debug.Log("Not moving.");
        }
        else
        {
            Vector3 oldPosition = cameraRig.position;
            Quaternion oldRotation = cameraRig.rotation;

            movement += cameraOrientation * new Vector3(axis.x, 0, axis.y) * Time.deltaTime;

            if (run == true)
            {
                //Debug.Log("Running.");
                player.position += (movement * (sensitivity * 2));
            }
            else
            {
                //Debug.Log("Walking.");
                player.position += (movement * sensitivity);
            }
        }
    }

    private void ToggleRunning()
    {
        bool doubleTap = trackPadRunning.GetState(SteamVR_Input_Sources.RightHand);

        if (doubleTap == true)
            run = !run;
    }

    private void ToggleTeleporting()
    {
        movementSwitch = movementToggle.GetState(SteamVR_Input_Sources.RightHand);

        if (movementSwitch == false)
            playerTeleportation.enabled = false;
        else
            playerTeleportation.enabled = true;
    }
}