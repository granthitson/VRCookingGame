using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class settingSinkWater : MonoBehaviour
{

    public Transform player;
    public CircularDrive waterKnob;
    private float knobAngle;
    public GameObject sinkWater;
    public Collider waterCollider;
    public GameObject potWater;
    

    // Start is called before the first frame update
    void Start()
    {
        sinkWater.SetActive(false);
    
    }

    // Update is called once per frame
    void Update()
    {
        knobAngle = waterKnob.outAngle;
        if (knobAngle > 1)
        {
            sinkWater.SetActive(true);
        }
        if (knobAngle < 1)
        {
            sinkWater.SetActive(false);
        }
            
    }

  




}
