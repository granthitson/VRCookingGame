using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillingPot : MonoBehaviour
{
    public GameObject potWater;

    public void Start()
    {
        potWater.SetActive(false);
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "sinkWater")
        {
            potWater.SetActive(true);
            raisePotWater();
        }

    }

    public void raisePotWater()
    {
        Vector3 yPosition = new Vector3(0f,.70f,0f);
        
        potWater.transform.localPosition = yPosition;

    }


}
