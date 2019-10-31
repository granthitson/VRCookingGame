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
    protected float cookingValue = 0.0f;

    [SerializeField]
    protected float cleanlinessValue = 0.0f;
    [SerializeField]
    protected bool hasTouchedFloor = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fridge")
            ;

    }

    private void OnTriggerExit(Collider other)
    {
        ;
    }
}