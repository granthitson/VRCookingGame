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
    private ParticleSystem butterParticles;

    private Dictionary<int, GameObject> listOfFoods;

    private void Awake()
    {
        butterParticles = GetComponentInChildren<ParticleSystem>();
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
        listOfFoods = new Dictionary<int, GameObject>();
    }

    private void Update()
    {
        HeatingCookware();

        if (butteredValue == 0f)
        {
            isButtered = false;
            if (butterParticles != null)
            {
                if (butterParticles.isPlaying)
                    butterParticles.Stop();
            }
        }
        else
        {
            if (butterParticles != null)
            {
                if (butterParticles.isStopped)
                    butterParticles.Play();

                ParticleSystem.ShapeModule shape = butterParticles.shape;
                if (shape.radius < .1)
                    shape.radius += (butteredValue * .001f);
            }
        }

        if (heatValue > 0 && listOfFoods.Count > 0)
        {
            foreach (KeyValuePair<int, GameObject> go in listOfFoods)
            {
                Food food = go.Value.GetComponent<Food>();
                if (food != null)
                {
                    float t = heatValue * .0005f;
                    float b = food.GetTemperatureValue();
                    if (b < 1.0f)
                        food.SetTemperatureValue(b + t);

                    if (food.usesButter && butteredValue > butteredValueMin)
                    {
                        butteredValue -= .0005f;
                        float temp = food.GetButterValue();
                        if (temp < 1f)
                            food.SetButterValue(temp + .0005f);

                        aSource.volume -= aSource.volume * .0002f;

                        if (butterParticles != null && butterParticles.isStopped)
                        {
                            butterParticles.Play();
                        }
                        

                        if (!aSource.isPlaying)
                        {
                            aSource.PlayOneShot(aSource.clip);
                        }
                    }
                }

                if (food is Butter)
                {
                    Butter butter = food.GetComponent<Butter>();
                    if (butteredValue < butteredValueMax && (isHeating || butter.GetTemperatureValue() > 0))
                    {
                        float bv = butter.GetButterValue();
                        float temp = .0009f * heatValue;
                        
                        butteredValue += temp;
                        butter.SetButterValue(bv -= temp);

                        if (aSource.volume < .3f)
                            aSource.volume += butteredValue * .0005f;
                    }
                    else if (butteredValue > butteredValueMax)
                    {
                        butteredValue = 1f;
                        if (isHeating)
                        {
                            float bv = butter.GetButterValue();
                            float temp = bv * .0007f * heatValue;
                            butter.SetButterValue(bv -= temp);
                            if (aSource.volume < .3f)
                                aSource.volume += butteredValue * .0005f;
                        }
                    }

                    if (butter.GetButterValue() > 0 && (isHeating || butter.GetTemperatureValue() > 0))
                    {
                        if (isButtered == false)
                            aSource.Play();
                    }
                    else
                    {
                        if (heatValue <= 0 && butteredValue > 0)
                            aSource.Stop();
                    }

                    if (butteredValue > 0)
                        isButtered = true;
                    else
                        isButtered = false;
                }
            }
        }

        if (heatValue > 0 && butteredValue > 0)
        {
            butteredValue -= heatValue * .000001f;
            if (butterParticles != null)
            {
                ParticleSystem.ShapeModule shape = butterParticles.shape;
                if (shape.radius > 0)
                    shape.radius -= (butteredValue * .001f);
            }
        }
    }

    private void HeatingCookware()
    {
        if (isHeating == false)
        {
            if (heatValue > 0)
                heatValue -= 0.00001f;
        }
        else
        {
            if (heatValue > 0)
            {
                heatValue += heatingElement.GetHeatAmount() * .00005f;
                heatValue = Mathf.Clamp(heatValue, minHeat, maxHeat);
            }
            else
                heatValue = .01f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (heatingElement == null)
        {
            heatingElement = other.GetComponent<HeatingElement>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject current = other.gameObject;

        if (heatingElement != null)
        {
            if (heatingElement.isTurnedOn() == true)
            {
                isHeating = true;
            }
            else
                isHeating = false;
        }
        else
        {
            heatingElement = current.GetComponent<HeatingElement>();
        }

        if (current.CompareTag("Food"))
        {
            BoxCollider bc = current.GetComponent<BoxCollider>();
            MeshCollider mc = current.GetComponent<MeshCollider>();
            if (bc == null && mc != null)
            {
                current.AddComponent<BoxCollider>();
                Destroy(mc);
            }

            if (!listOfFoods.ContainsKey(current.GetInstanceID()))
            {
                listOfFoods.Add(current.GetInstanceID(), current);
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject current = other.gameObject;

        if (current.CompareTag("HeatingElement"))
        {
            isHeating = false;
            heatingElement = null;
        }
        else if (current.CompareTag("Food"))
        {
            BoxCollider bc = current.GetComponent<BoxCollider>();
            MeshCollider mc = current.GetComponent<MeshCollider>();
            if (bc != null && mc == null)
            {
                MeshCollider m = current.AddComponent<MeshCollider>();
                m.convex = true;
                Destroy(bc);
            }

            if (listOfFoods.ContainsKey(current.GetInstanceID()))
                listOfFoods.Remove(current.GetInstanceID());
        }
    }

    public float GetHeatValue()
    {
        return heatValue;
    }

    public void RemoveFromListOfAllFoods(int id)
    {
        listOfFoods.Remove(id);
    }
}
