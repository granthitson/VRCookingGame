using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject tutorialMenu;

    private void Start()
    {
        if (SceneHandler.sceneH.playedTutorial == false)
        {
            tutorialMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneHandler.sceneH.playedTutorial == true)
        {
            tutorialMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
