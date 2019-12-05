using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingSystem : MonoBehaviour
{
    private Recipe current;
    private GameObject currentPlate;

    private List<GameObject> plates;
    private List<int> completedPlates;

    // Start is called before the first frame update
    void Start()
    {
        current = CurrentRecipe.instance.GetCurrentRecipe();
        plates = new List<GameObject>();
        completedPlates = new List<int>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("plate entered ?");
        if (other.GetComponent<Plate>() != null)
        {
            currentPlate = other.gameObject;
            if (completedPlates.Contains(currentPlate.GetInstanceID()))
            {
                Debug.Log(other.name + " already rated");
            }
            else
            {
                float cleanliness = 0f;
                float cookingValue = 0f;
                float temperature = 0f;
                int counter = 0;

                foreach (var food in other.GetComponent<Plate>().addedFood)
                {
                    Debug.Log(food.name);
                    counter += 1;
                    cleanliness += food.GetComponent<Food>().GetCleanlinessValue();
                    cookingValue += food.GetComponent<Food>().GetCookingValue();
                    temperature += food.GetComponent<Food>().GetTemperatureValue();
                }

                cleanliness /= counter;
                cookingValue /= counter;
                temperature /= counter;

                other.GetComponent<Plate>().cookingScore = cookingValue;
                other.GetComponent<Plate>().temperatureScore = temperature;
                other.GetComponent<Plate>().cleanlinessScore = cleanliness;

                //Debug.Log(cleanliness + " " + temperature + " " + cookingValue);
                completedPlates.Add(other.GetInstanceID());
            }
        } 
    }

}
