using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrilledCheese : Recipe
{
    // Start is called before the first frame update
    void Start()
    {
        acceptedFoods = new List<string>();
        acceptedFoods.Add("Cheese");
        acceptedFoods.Add("Tomato");
        acceptedFoods.Add("Bread");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
