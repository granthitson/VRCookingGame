using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class HeatingElement : MonoBehaviour
{
    public CircularDrive heatKnob;
    public Canvas knobUI;

    public AudioClip sparkFlame;
    public AudioClip sparkFinish;

    private Transform player;

    private AudioSource aSource;
    private BoxCollider heatingElement;

    private Light flame;
    private ParticleSystem[] blueFlames;
    private Transform[] blueFlamesTransform;

    private Vector3 flameScale;
    private float flameScaleX;
    private float flameScaleY;
    private float flameScaleZ;
    private Vector3 flameScaleMax = new Vector3(0.002721404f, 0.002721403f, 0.0009831772f);
    private Vector3 flameScaleMin = new Vector3(0.0007806076f, 0.0007806073f, 0.0002820145f);

    private GameObject temperatureUI;
    private Text temperature;

    private bool heatElementToggle;
    private bool oven = false;
    private float knobAngle;

    [SerializeField]
    private float heatAmount;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerHead").GetComponent<Transform>();

        heatingElement = GetComponent<BoxCollider>();
        if (heatingElement.name == "Oven Burner")
        {
            oven = true;
        }

        flame = GetComponent<Light>();
        temperature = knobUI.GetComponentInChildren<Text>();
        temperatureUI = knobUI.gameObject;

        blueFlames = GetComponentsInChildren<ParticleSystem>();
        blueFlamesTransform = GetComponentsInChildren<Transform>();

        aSource = GetComponent<AudioSource>();
        aSource.minDistance = .25f;
        aSource.maxDistance = .4f;

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
            heatAmount = Mathf.Abs(((knobAngle * 3000f) / 345f) / 300);
            temperature.text = Mathf.Round(heatAmount) + "";
            if (Mathf.Abs(knobAngle) < 30)
                aSource.volume = .25f;
            else
                aSource.volume = .25f + Mathf.Abs((knobAngle / 129) * .15f);
        }
        else
        {
            //angle to heat measurement - 450 farenheit - 345 degrees of rotation - scale of 1 to 450
            heatAmount = Mathf.Abs(((knobAngle * 450f) / 345f));
            temperature.text = Mathf.Round(heatAmount) + " F";
        }

        if (Mathf.Abs(knobAngle) > 30)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public float GetHeatAmount()
    {
        return heatAmount;
    }

    public bool isTurnedOn()
    {
        return heatElementToggle;
    }

    public void TurnOn()
    {
        //Debug.Log("Heating Pot or Pan to " + heatAmount);
        if (heatElementToggle == false)
        {
            aSource.loop = false;
            aSource.PlayOneShot(sparkFlame);
            aSource.loop = true;
            aSource.clip = sparkFinish;
            aSource.Play();
            Debug.Log(gameObject.name + " audio playing");
        }
        heatElementToggle = true;
        flame.enabled = true;
        knobUI.enabled = true;
        temperatureUI.transform.LookAt(player);
        if (oven == false)
        {
            flameOn();
            flameRegulate();
        }
    }

    public void TurnOff()
    {
        aSource.Stop();
        heatElementToggle = false;
        flame.enabled = false;
        knobUI.enabled = false;
        if (oven == false)
        {
            flameOff();
            flameRegulate();
        }
    }

    private void flameOn()
    {
        foreach (ParticleSystem p in blueFlames)
            if (p.isStopped)
                p.Play();
    }

    private void flameOff()
    {
        foreach (ParticleSystem p in blueFlames)
            if (p.isPlaying)
                p.Stop();
    }

    private void flameRegulate()
    {
        foreach (Transform t in blueFlamesTransform)
        {
            if (t.gameObject.name == "Particle System")
            {
                if (heatAmount >= 1)
                {
                    flameScaleX = flameScaleMin.x * heatAmount / 3;
                    flameScaleX = Mathf.Clamp(flameScaleX, flameScaleMin.x, flameScaleMax.x);

                    flameScaleY = flameScaleMin.y * heatAmount / 3;
                    flameScaleY = Mathf.Clamp(flameScaleY, flameScaleMin.y, flameScaleMax.y);

                    flameScaleZ = flameScaleMin.z * heatAmount / 3;
                    flameScaleZ = Mathf.Clamp(flameScaleZ, flameScaleMin.z, flameScaleMax.z);

                    flameScale = new Vector3(flameScaleX,flameScaleY,flameScaleZ);
                    t.localScale = flameScale;
                }
            }
        }
    }
}
