using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class HeatingElement : MonoBehaviour
{
    public Transform player;
    public CircularDrive heatKnob;
    public Canvas knobUI;

    private CapsuleCollider heatingElement;
    private BoxCollider heatingElementOven;
    private Light flame;

    private GameObject temperatureUI;
    private Text temperature;

    private bool heatElementToggle;
    private bool oven = false;
    private float knobAngle;
    private float heatAmount;


    // Start is called before the first frame update
    void Start()
    {
        heatingElement = GetComponent<CapsuleCollider>();
        if (heatingElement == null)
        {
            heatingElementOven = GetComponent<BoxCollider>();
            oven = true;
        }

        flame = GetComponent<Light>();
        temperature = knobUI.GetComponentInChildren<Text>();
        temperatureUI = knobUI.gameObject;


        heatElementToggle = false;
        flame.enabled = false;
        knobUI.enabled = false;
    }

    private void Update()
    {
        knobAngle = heatKnob.outAngle;
        if (oven == false)
        {
            //angle to heat measurement - 3000 farenheit - 345 degrees of rotation - scale of 1 to 10
            heatAmount = Mathf.Abs(Mathf.Floor(((knobAngle * 3000f) / 345f) / 300));
            temperature.text = heatAmount + "";
        }
        else
        {
            //angle to heat measurement - 450 farenheit - 345 degrees of rotation - scale of 1 to 450
            heatAmount = Mathf.Abs(Mathf.Floor(((knobAngle * 450f) / 345f)));
            temperature.text = heatAmount + " F";
        }

        if (Mathf.Abs(knobAngle) > 10)
            TurnOn();
        else
            TurnOff();
    }

    public void TurnOn()
    {
        //Debug.Log("Heating Pot or Pan to " + heatAmount);
        heatElementToggle = true;
        flame.enabled = true;
        knobUI.enabled = true;
        temperatureUI.transform.LookAt(player);
    }

    public void TurnOff()
    {
        heatElementToggle = false;
        flame.enabled = false;
        knobUI.enabled = false;
    }
}
