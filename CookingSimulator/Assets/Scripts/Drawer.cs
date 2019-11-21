using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Drawer : MonoBehaviour
{
    BoxCollider contents;

    public AudioClip open;
    public AudioClip close;

    private LinearMapping linearMap;
    private AudioSource aSource;

    private bool openState;

    // Start is called before the first frame update
    void Start()
    {
        contents = GetComponent<BoxCollider>();
        linearMap = GetComponent<LinearMapping>();
        aSource = GetComponent<AudioSource>();

        if (linearMap.value > .98f)
            openState = true;
        else
            openState = false;
    }

    void Update()
    {
        if (linearMap.value < .15f)
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

    private void ChangeState()
    {
        openState = !openState;
        if (openState)
        {
            Debug.Log(openState + " playing open");
            aSource.PlayOneShot(open);
        }
        else
        {
            Debug.Log(openState + " playing close");
            aSource.clip = close;
            aSource.PlayOneShot(close);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerHand")
        {
            Rigidbody p = other.GetComponentInParent<Rigidbody>();
            if (p != null)
            {
                p.transform.parent = gameObject.transform;
            }
            else
                other.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "PlayerHand")
        {
            Rigidbody p = other.GetComponentInParent<Rigidbody>();
            if (p != null)
            {
                p.transform.parent = null;
            }
            else
                other.transform.parent = null;
        }
    }
}
