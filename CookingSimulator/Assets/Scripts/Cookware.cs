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
    private float butteredValue = 0f;

    private float butteredValueMax = 1f;
    private float butteredValueMin = 0f;

    private AudioSource aSource;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
        if (aSource == null)
        {
            aSource = gameObject.AddComponent<AudioSource>();
            aSource.loop = true;
            aSource.volume = .01f;
            aSource.spatialBlend = 1.0f;
            aSource.playOnAwake = false;
        }
        aSource.clip = sizzling;
    }

    private void Start()
    {
        heatValueText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (isHeating == false && heatValue > 0)
            heatValue -= 0.00001f;

        if (heatValueText != null)
            heatValueText.text = heatValue.ToString();

        if (butteredValue == 0f)
            isButtered = false;
    }

    private void OnTriggerStay(Collider other)
    {
        heatingElement = other.gameObject.GetComponent<HeatingElement>();

        if (heatingElement != null)
        {
            if (heatingElement.isTurnedOn() == true)
            {
                isHeating = true;
                heatValue += heatingElement.GetHeatAmount() * .00001f;
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
                float t = heatValue * .0001f;
                float b = other.gameObject.GetComponent<Food>().GetTemperatureValue();
                if (b < 1.0f)
                    other.gameObject.GetComponent<Food>().SetTemperatureValue(b + t);

                if (other.GetComponent<Food>().usesButter && butteredValue > butteredValueMin)
                {
                    butteredValue -= .0005f;
                    float temp = other.GetComponent<Food>().GetButterValue();
                    if (temp < 1f)
                        other.GetComponent<Food>().SetButterValue(temp + .0001f);

                    aSource.volume -= aSource.volume * .0002f;

                    if (!aSource.isPlaying)
                    {
                        aSource.PlayOneShot(aSource.clip);
                    }
                }
                    
            }

            if (other.GetComponent<Butter>() != null)
            {
                if (butteredValue < butteredValueMax && (isHeating || other.GetComponent<Butter>().GetTemperatureValue() > 0))
                {
                    float bv = other.GetComponent<Butter>().GetButterValue();
                    float temp = bv * .0005f * heatValue;
                    butteredValue += temp;
                    other.GetComponent<Butter>().SetButterValue(bv -= temp);
                    if (aSource.volume < .3f)
                        aSource.volume += butteredValue * .0005f;
                }
                else if (butteredValue > butteredValueMax)
                {
                    butteredValue = 1f;
                    if (isHeating)
                    {
                        float bv = other.GetComponent<Butter>().GetButterValue();
                        float temp = bv * .0005f * heatValue;
                        other.GetComponent<Butter>().SetButterValue(bv -= temp);
                        if (aSource.volume < .3f)
                            aSource.volume += butteredValue * .0005f;
                    }
                }

                if (other.GetComponent<Butter>().GetButterValue() > 0 && (isHeating || other.GetComponent<Butter>().GetTemperatureValue() > 0))
                {
                    if (isButtered == false)
                    {
                        Debug.Log("sizzling");
                        aSource.Play();
                    }
                }
                else
                {
                    if (heatValue > 0 && butteredValue > 0)
                    {
                        ;
                    }
                    else
                    {
                        Debug.Log("not sizzling");
                        aSource.Stop();
                    }
                }

                if (butteredValue > 0)
                    isButtered = true;
                else
                    isButtered = false;
            }
        }

        if (heatValue > 0 && butteredValue > 0)
        {
            butteredValue -= heatValue * .000001f;
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
