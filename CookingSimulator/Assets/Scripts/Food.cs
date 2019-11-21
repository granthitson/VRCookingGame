using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    protected bool Cookable = true;
    [SerializeField]
    protected bool Freezable = true;
    [SerializeField]
    protected bool Cuttable = true;

    [SerializeField]
    protected float temperatureValue = 0.0f;
    [SerializeField]
    protected bool isCooking = false;
    protected bool isBurnt = false;
    protected bool isCooked = false;

    [SerializeField]
    protected float cookingValue = 0.0f;

    [SerializeField]
    protected float cleanlinessValue = 0.0f;
    [SerializeField]
    protected bool hasTouchedFloor = false;

    protected float xScale;
    protected float yScale;
    protected float zScale;

    protected bool wakeup;
    protected bool onPan;
    protected Collider oth;

    public Material capMaterial;
    public bool usesButter = true;
    [SerializeField]
    protected float butterValue = 0f;

    protected void WakeUp()
    {
        Rigidbody rB = GetComponent<Rigidbody>();
        rB.WakeUp();
    }

    private void Update()
    {
        WakeUp();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Cookware temp = other.GetComponent<Cookware>();
        if (temp != null)
        {
            onPan = true;
            wakeup = true;
            oth = other;
            if (temp.GetHeatValue() > 0 || temperatureValue > 0)
            {
                isCooking = true;
            }
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        WakeUp();

        if (other.tag == "Floor")
        {
            hasTouchedFloor = true;
        }
        else if (other.tag == "Pot" || other.tag == "Pan")
        {
            ;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Cookware>() != null)
        {
            isCooking = false;
            onPan = false;
            wakeup = false;
        }
    }

    public float GetCleanlinessValue()
    {
        return cleanlinessValue;
    }

    public void SetCleanlinessValue(float v)
    {
        cleanlinessValue = v; ;
    }

    public float GetTemperatureValue()
    {
        return temperatureValue;
    }

    public void SetTemperatureValue(float v)
    {
        temperatureValue = v;
    }

    public float GetButterValue()
    {
        return butterValue;
    }

    public void SetButterValue(float v)
    {
        butterValue = v;
    }

    public float GetCookingValue()
    {
        return cookingValue;
    }
}