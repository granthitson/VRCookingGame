using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string current;
    public string next;

    public void loadLevel() {
        SceneManager.UnloadSceneAsync(current);
        Debug.Log("Unloaded " + current);
        SceneManager.LoadScene(next);
        Debug.Log("Loaded " + next);
    }
}
