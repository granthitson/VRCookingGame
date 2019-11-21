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

    [SerializeField]
    protected float cookingValue = 0.0f;

    [SerializeField]
    protected float cleanlinessValue = 0.0f;
    [SerializeField]
    protected bool hasTouchedFloor;

    protected float xScale;
    protected float yScale;
    protected float zScale;

    protected bool onPan;
    protected Collider oth;

    public Material capMaterial;
    public bool usesButter = true;


    public void SetTemperatureValue(float v)
    {
        temperatureValue = v;
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FoodCollider"))
        {
            oth = other;
            onPan = true;
            isCooking = true;
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (other.tag == "Knife")
        {
            Rigidbody rB = GetComponent<Rigidbody>();
            rB.WakeUp();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("FoodCollider"))
        {
            isCooking = false;
            onPan = false;
        }
    }
}