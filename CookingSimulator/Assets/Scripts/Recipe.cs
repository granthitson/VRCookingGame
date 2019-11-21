using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    protected string recipeName;

    protected List<string> acceptedFoods;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetRecipeName()
    {
        return recipeName;
    }

    public List<string> GetAcceptableFoods()
    {
        return acceptedFoods;
    }
}
