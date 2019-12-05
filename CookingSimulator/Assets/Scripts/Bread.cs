using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Bread : Food
{
    public Material transitionStageOne;
    public Material transitionStageTwo;
    public Material transitionStageThree;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        canCombine = true;
    }

    protected override void CookingUpdate()
    {
        if (isCooking)
        {
            float temp = 0f;

            if (cookingValue < .5f)
            {
                rend.material = transitionStageOne;
                temp = (Mathf.Round(cookingValue * 100) / 100f) * 2;
            }
            else if (cookingValue > .5f && cookingValue < 1f)
            {
                rend.material = transitionStageTwo;
                temp = (Mathf.Round((cookingValue - .5f) * 100) / 100f) * 2;
            }
            else if (cookingValue > 1f)
            {
                rend.material = transitionStageThree;
                temp = (Mathf.Round((cookingValue - 1f) * 100) / 100f) * 2;
            }

            rend.material.SetFloat("_LerpValue", temp);
            //Debug.Log(rend.material.GetFloat("_LerpValue"));
        }
    }
}
