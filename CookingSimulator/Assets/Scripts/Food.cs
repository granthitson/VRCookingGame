using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private bool Cookable = true;
    [SerializeField]
    private bool Freezable = true;
    [SerializeField]
    private bool Cuttable = true;

    [SerializeField]
    private float temperatureValue = 0.0f;

    [SerializeField]
    private float cookingValue = 0.0f;

    [SerializeField]
    private float cleanlinessValue = 0.0f;
    [SerializeField]
    private bool hasTouchedFloor = false;


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