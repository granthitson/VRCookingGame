using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    public Button endTutorial;
    public Button resetButton;

    void Update()
    {
        if (SceneHandler.sceneH.playedTutorial == true)
        {
            if (endTutorial != null)
            {
                EnableButton(endTutorial);
                EnableButton(resetButton);
            }
        }
    }

    public void TutorialReset()
    {
        SceneHandler.sceneH.TutorialReset();
    }

    public void EndTutorial()
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
