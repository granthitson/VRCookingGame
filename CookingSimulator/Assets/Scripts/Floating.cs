using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float variation = .05f;
    private Vector3 initial;
    private Vector3 initialRotation;

    private float maxHeight;
    private float minHeight;

    private float hoverHeight;
    private float hoverRange;
    private float hoverSpeed = 1f;

    void Awake()
    {
        initial = transform.position;
    }

    void Start()
    {
        if (SceneHandler.sceneH.playedTutorial == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            initialRotation = transform.eulerAngles;

            maxHeight = initial.y + variation;
            minHeight = initial.y - variation;

            hoverHeight = (maxHeight + minHeight) / 2.0f;
            hoverRange = maxHeight - minHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 temp = Vector3.up * Mathf.Cos(Time.time);
       // transform.position = temp + initial;
        Vector3 temp = Vector3.up * (hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange);
        transform.position = initial + temp;
    }

    public void Revert()
    {
        transform.eulerAngles = initialRotation;
        transform.position = initial;
    }
}
