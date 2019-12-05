using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    public GameObject tv;
    public List<GameObject> remotes;
    public List<GameObject> toCleanUp;
    public List<GameObject> dishes;
    public List<GameObject> rotting;
    public GameObject dishsoap;
    public GameObject broom;
    public GameObject trashcan;
    public GameObject fridge;
    public GameObject recipesystem;
    public GameObject instruction;
    public GameObject ordersystem;
    public GameObject order;
    public GameObject stove;
    public GameObject butter;
    public GameObject combine;
    public GameObject pan;
    public GameObject knife;
    public GameObject diningtable;
    
    public Button endTutorial;
    public Button resetButton;

    public Alternate remoteAlternate;

    private bool start = true;
    private bool hasInstruction;
    private bool waitingForPlayer;
    private bool end = false;


    private void HighlightObject(GameObject go, Color? c = null)
    {
        Outline o = go.GetComponent<Outline>();

        if (o == null)
        {
            var outline = go.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = c ?? (Color.yellow + Color.red);
            outline.OutlineWidth = 5f;
        }
        else
        {
            o.enabled = true;
        }
    }

    private void UnHighlightObject(GameObject go)
    {
        go.GetComponent<Outline>().enabled = false;
    }


    void Start()
    {
        if (start)
        {
            HighlightObject(tv);
        }

        if (end)
        {
            if (SceneHandler.sceneH.playedTutorial == true)
            {
                if (end && endTutorial != null)
                {
                    EnableButton(endTutorial);
                    EnableButton(resetButton);
                }
            }
        }
    }

    public void RemoteTutorial()
    {
        Debug.Log("Unhighlighting tv");
        UnHighlightObject(tv);

        foreach (GameObject remote in remotes)
        {
            Debug.Log("Highlighting " + remote.name);
            HighlightObject(remote);
        }

        remoteAlternate.enabled = true;
    }

    public void CleanTutorial()
    {
        remoteAlternate.enabled = false;

        foreach (GameObject remote in remotes)
        {
            Debug.Log("Unhighlighting " + remote.name);
            UnHighlightObject(remote);
        }
    }

    public void DishTutorial()
    {
        foreach (GameObject trash in toCleanUp)
        {
            Debug.Log("Highlighting " + trash.name);
            HighlightObject(trash);
        }
    }

    //public void DishsoapTutorial()
    //{
    //    foreach (GameObject dish in dishes)
    //    {
    //        Debug.Log("Unhighlighting " + dish.name);
    //        UnHighlightObject(dish);
    //    }

    //    HighlightObject(dishsoap);
    //}

    //public void BreakTutorial()
    //{
    //    UnHighlightObject(dishsoap);
    //}

    //public void BroomTutorial()
    //{
    //    HighlightObject(broom);
    //}

    //public void TrashTutorial()
    //{
    //    UnHighlightObject(broom);
    //    HighlightObject(trashcan);
    //}

    //public void TemperatureTutorial()
    //{
    //    UnHighlightObject(trashcan);
    //    HighlightObject(fridge);
    //}

    //public void RottingTutorial()
    //{
    //    UnHighlightObject(fridge);

    //    foreach (GameObject food in rotting)
    //    {
    //        Debug.Log("Unhighlighting " + food.name);
    //        HighlightObject(food);
    //    }
    //}

    public void RecipeTutorial()
    {
        //foreach (GameObject food in rotting)
        //{
        //    Debug.Log("Unhighlighting " + food.name);
        //    UnHighlightObject(food);
        //}

        foreach (GameObject trash in toCleanUp)
        {
            Debug.Log("Unhighlighting " + trash.name);
            UnHighlightObject(trash);
        }

        HighlightObject(recipesystem);
    }

    public void InstructionTutorial()
    {
        UnHighlightObject(recipesystem);
        HighlightObject(instruction);
    }

    public void MaterialsTutorial()
    {
        UnHighlightObject(instruction);
        HighlightObject(ordersystem);
    }

    public void OrderTutorial()
    {
        UnHighlightObject(ordersystem);
        HighlightObject(order);
    }

    public void HeatingTutorial()
    {
        UnHighlightObject(order);
        HighlightObject(stove);
    }

    public void ButterTutorial()
    {
        UnHighlightObject(stove);
        HighlightObject(butter);
    }

    public void CombineTutorial()
    {
        UnHighlightObject(ordersystem);
        HighlightObject(combine);
    }

    public void CookingTutorial()
    {
        UnHighlightObject(knife);
        HighlightObject(pan);
        HighlightObject(stove);
    }

    public void CuttingTutorial()
    {
        UnHighlightObject(combine);
        HighlightObject(knife);
    }

    public void DeliveryTutorial()
    {
        UnHighlightObject(pan);
        UnHighlightObject(stove);

        foreach (GameObject dish in dishes)
        {
            Debug.Log("Highlighting " + dish.name);
            HighlightObject(dish);
        }
    }

    public void RatingTutorial()
    {
        foreach (GameObject dish in dishes)
        {
            Debug.Log("Ungighlighting " + dish.name);
            UnHighlightObject(dish);
        }

        HighlightObject(diningtable);
    }

    public void EndTutorial()
    {
        UnHighlightObject(diningtable);
    }

    public void TutorialReset()
    {
        SceneHandler.sceneH.TutorialReset();
    }

    public void QuitTutorial()
    {
        SceneHandler.sceneH.EndTutorial();
    }

    public void QuitGame()
    {
        SceneHandler.sceneH.QuitGame();
    }

    public void EnableButton(Button b)
    {
        b.interactable = true;
    }
}
