using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingSystem : MonoBehaviour
{
    private Recipe current;
    private GameObject currentPlate;

    private List<GameObject> plates;

    // Start is called before the first frame update
    void Start()
    {
        current = CurrentRecipe.instance.GetCurrentRecipe();
        plates = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(currentPlate);
        if (other.GetComponent<Plate>() != null)
        {
            currentPlate = other.gameObject;
            plates.Add(currentPlate);
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Plate>() != null)
        {
            foreach (var plate in plates)
            {
                float cleanliness = 0f;
                float cookingValue = 0f;
                float temperature = 0f;
                int counter = 0;

                foreach (var food in plate.GetComponent<Plate>().addedFood)
                {
                    counter += 1;
                    cleanliness += food.GetComponent<Food>().GetCleanlinessValue();
                    cookingValue += food.GetComponent<Food>().GetCookingValue();
                    temperature += food.GetComponent<Food>().GetTemperatureValue();
                }

                cleanliness /= counter;
                cookingValue /= counter;
                temperature /= counter;

                plate.GetComponent<Plate>().cookingScore = cookingValue;
                plate.GetComponent<Plate>().temperatureScore = temperature;
                plate.GetComponent<Plate>().cleanlinessScore = cleanliness;

                Debug.Log(cleanliness + " " + temperature + " " + cookingValue);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
