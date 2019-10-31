using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class Cookware : MonoBehaviour
{
    [SerializeField]
    private float heatValue = 0;

    private float maxHeat = 1;
    private float minHeat = 0;

    private HeatingElement heatingElement;
    private Text heatValueText;

    [SerializeField]
    private bool isHeating;

    private void Start()
    {
        heatValueText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (isHeating == false && heatValue > 0)
            heatValue -= 0.0000001f;

        if (heatValueText != null)
            heatValueText.text = heatValue.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        heatingElement = other.gameObject.GetComponent<HeatingElement>();

        if (heatingElement != null)
        {
            if (heatingElement.isTurnedOn() == true)
            {
                isHeating = true;
                heatValue += heatingElement.GetHeatAmount() * .0000001f;
                heatValue = Mathf.Clamp(heatValue, minHeat, maxHeat);
            }
            else 
            {
                isHeating = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHeating = false;
    }

    public float GetHeatValue()
    {
        return heatValue;
    }
}
