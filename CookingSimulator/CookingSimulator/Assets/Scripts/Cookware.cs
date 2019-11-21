using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class Cookware : MonoBehaviour
{
    public AudioClip sizzling;

    [SerializeField]
    private float heatValue = 0;

    private float maxHeat = 1;
    private float minHeat = 0;

    private HeatingElement heatingElement;
    private Text heatValueText;

    [SerializeField]
    private bool isHeating;

    [SerializeField]
    private bool isButtered;
    [SerializeField]
    private float butteredValue;

    private float butteredValueMax;
    private float butteredValueMin;

    private AudioSource aSource;

    private void Awake()
    {
        if (aSource == null)
            aSource = gameObject.AddComponent<AudioSource>();

        aSource.loop = true;
        aSource.volume = .2f;
        aSource.spatialBlend = 1.0f;
        aSource.playOnAwake = false;
    }

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
        
        if (other.tag == "Food")
        {
            if (other.GetComponent<Food>() != null)
            {
                Debug.Log(other.name);
                other.gameObject.GetComponent<Food>().SetTemperatureValue(heatValue);
                if (other.GetComponent<Food>().usesButter)
                    butteredValue -= .001f;
            }

            if (other.GetComponent<Butter>() != null)
            {
                isButtered = true;
                if (butteredValue < butteredValueMax)
                    butteredValue = other.GetComponent<Butter>().butterValue;

                Debug.Log(butteredValue);
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
