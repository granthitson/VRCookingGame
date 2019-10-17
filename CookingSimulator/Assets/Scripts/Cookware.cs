using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookware : MonoBehaviour
{
    [SerializeField]
    private float heatValue = 0;

    private float maxHeat = 1;
    private float minHeat = 0;

    private HeatingElement heatingElement;

    private bool heating;

    private void Update()
    {
        if (heating == false)
            heatValue -= 0.0000001f;
    }

    private void OnTriggerStay(Collider other)
    {
        heatingElement = other.gameObject.GetComponent<HeatingElement>();

        if (heatingElement != null)
        {
            if (heatingElement.isTurnedOn() == true)
            {
                heating = true;
                heatValue += heatingElement.GetHeatAmount() * .0000001f;
                heatValue = Mathf.Clamp(heatValue, minHeat, maxHeat);
            }
            else 
            {
                heating = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        heating = false;
    }
}
