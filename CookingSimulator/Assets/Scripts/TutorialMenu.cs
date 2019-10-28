using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
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
}
