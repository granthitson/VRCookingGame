using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRecipe : MonoBehaviour
{
    public static CurrentRecipe instance;

    [SerializeField]
    private string recipeName;

    private Recipe current;
    private bool recipeLock;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        recipeLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recipeName != null && recipeLock == false)
        {
            if (recipeName == "Grilled Cheese")
            {
                gameObject.AddComponent<GrilledCheese>();
                current = GetComponent<GrilledCheese>();
                recipeLock = true;
            }
        }
    }

    public Recipe GetCurrentRecipe()
    {
        return current;
    }

    public void SetCurrentRecipe(string r)
    {
        recipeName = r;
    }

}
