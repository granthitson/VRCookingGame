using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Material transitionStageOne;
    public Material transitionStageTwo;
    public Material transitionStageThree;

    private Renderer rend;

    private float cookingValue = 0f;
    private bool isCooking = true;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("blendShader");
    }

    // Update is called once per frame
    void Update()
    {
        //WakeUp();
        if (isCooking)
        {
            cookingValue += .001f;
            if (cookingValue < .5f)
            {
                Debug.Log(1);
                rend.material = transitionStageOne;
            }
            else if (cookingValue > .5f && cookingValue < 1f)
            {
                Debug.Log(2);
                rend.material = transitionStageTwo;
            }
            else if (cookingValue > 1f)
            {
                Debug.Log(3);
                rend.material = transitionStageThree;
            }
            Debug.Log(rend.material.GetFloat("_LerpValue"));
            rend.material.SetFloat("_LerpValue", cookingValue);
        }
    }
}
