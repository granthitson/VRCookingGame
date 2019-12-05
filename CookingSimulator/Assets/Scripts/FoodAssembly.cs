using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class FoodAssembly : MonoBehaviour
{
    private List<int> inhabitants;

    // Start is called before the first frame update
    void Start()
    {
        inhabitants = new List<int>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int id = other.GetInstanceID();
        if (other.CompareTag("Food") && !inhabitants.Contains(id))
        {
            Food food = other.GetComponent<Food>();

            if (food.GetCanCombine())
            {
                food.SetOnAssembler(true);
                inhabitants.Add(id);
                if (food.GetIsCombined())
                {
                    food.SetIsCombined(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int id = other.GetInstanceID();
        if (other.CompareTag("Food") && inhabitants.Contains(id))
        {
            Food food = other.GetComponent<Food>();

            if (food.GetCanCombine())
            {
                food.SetOnAssembler(true);
                inhabitants.Remove(id);

                if (!food.GetIsCombined())
                {
                    food.SetIsCombined(true);
                }
            }
        }
    }
}
