using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    [SerializeField]
    private Recipe selectedRecipe;
    public GameObject scoreCanvas;
    public List<GameObject> addedFood;

    public Transform parentPoint;

    public float cookingScore = -1f;
    public float temperatureScore = -1f;
    public float cleanlinessScore = -1f;

    public Text score;
    public Text cook;
    public Text clean;
    public Text temp;

    private void Start()
    {
        addedFood = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Recipe r = CurrentRecipe.instance.GetCurrentRecipe();
        if (r != null)
        {
            selectedRecipe = r;
        }

        if (cookingScore > 0 && temperatureScore > 0 && cleanlinessScore > 0)
        {
            cook.text = cookingScore + "/1";
            temp.text = temperatureScore + "/1";
            clean.text = cleanlinessScore + "/1";
            score.text = "Score: " + (cookingScore + temperatureScore + cleanlinessScore) / 3;
            scoreCanvas.SetActive(true);
        }
        else
        {
            scoreCanvas.SetActive(false);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (selectedRecipe != null && other.tag == "Food")
        {
            foreach (var name in selectedRecipe.GetAcceptableFoods())
            {
                if (other.name == name)
                {
                    other.transform.parent = parentPoint;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    other.GetComponent<Rigidbody>().useGravity = false;
                    addedFood.Add(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (selectedRecipe != null)
        {
            if (other.tag == "Food")
            {
                other.transform.parent = null;
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Rigidbody>().useGravity = true;
                addedFood.Remove(other.gameObject);
            }
        }
    }
}
