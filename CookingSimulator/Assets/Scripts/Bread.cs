using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Food
{
    public Material transitionStageOne;
    public Material transitionStageTwo;
    public Material transitionStageThree;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("blendShader");
    }

    // Update is called once per frame
    void Update()
    {
        WakeUp();
        if (isCooking)
        {
            float temp = 0f;

            cookingValue += .001f;
            if (cookingValue < .5f)
            {
                rend.material = transitionStageOne;
                temp = cookingValue * 2;
            }
            else if (cookingValue > .5f && cookingValue < 1f)
            {
                rend.material = transitionStageTwo;
                temp = (cookingValue-.5f) * 2f;
            }
            else if (cookingValue > 1f)
            {
                rend.material = transitionStageThree;
                temp = (cookingValue - 1f) * 2f;
            }

            rend.material.SetFloat("_LerpValue", temp);
            Debug.Log(rend.material.GetFloat("_LerpValue"));
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
            if (temperatureValue > 0 && butterValue > 0)
            {
                cookingValue += temperatureValue * .0001f;
                butterValue -= temperatureValue * .00001f;
            }
        }
        else if (other.name == "Cheese")
        {
            other.transform.parent = transform;
        }
    }
}
