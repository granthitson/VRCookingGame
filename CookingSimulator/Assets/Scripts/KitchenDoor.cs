using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class KitchenDoor : MonoBehaviour
{
    public bool fridge;
    public bool oven;

    public AudioClip open;
    public AudioClip close;

    private AudioSource aSource;

    private CircularDrive cDrive;
    private HingeJoint hinge;
    private JointSpring hingeSpring;
    private Rigidbody rB;
    private LinearMapping linearMap;

    private float minAngle;
    private float spring;

    private bool openState;

    // Start is called before the first frame update
    void Start()
    {
        cDrive = GetComponent<CircularDrive>();
        hinge = GetComponent<HingeJoint>();
        rB = GetComponent<Rigidbody>();
        linearMap = GetComponent<LinearMapping>();
        aSource = GetComponent<AudioSource>();

        if (fridge)
        {
            minAngle = 80;
            spring = .1f;
        }
        else if (oven)
        {
            minAngle = 35;
            spring = 30f;
        }
        else
        {
            minAngle = 20;
            spring = 5f;
        }

        if (Mathf.Abs(hinge.angle) > Mathf.Abs(minAngle))
            openState = true;
        else
            openState = false;

        hingeSpring.spring = spring;
    }

    void Update()
    {
        if (fridge)
        {
            if (Mathf.Abs(hinge.angle) < 5f)
            {
                if (openState)
                    ChangeState();
            }
            else
            {
                if (openState == false)
                    ChangeState();
            }
        }
        else if (oven)
        {
            if (Mathf.Abs(hinge.angle) < 10f)
            {
                if (openState)
                    ChangeState();
            }
            else
            {
                if (openState == false)
                    ChangeState();
            }
        }
        else
        {
            if (Mathf.Abs(hinge.angle) < Mathf.Abs(minAngle))
            {
                hinge.useSpring = true;
                hinge.spring = hingeSpring;
                if (openState)
                    ChangeState();
            }
            else
            {
                hinge.useSpring = false;
                if (openState == false)
                    ChangeState();
            }
        }
    }

    public void UpdateOutAngle()
    {
        cDrive.outAngle = Mathf.Abs(hinge.angle);
    }

    private void ChangeState()
    {
        openState = !openState;
        if (openState)
        {
            aSource.PlayOneShot(open);
        }
        else
        {
            aSource.clip = close;
            aSource.PlayOneShot(close);
        }
    }
}
