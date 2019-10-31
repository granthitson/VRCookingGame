using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler sceneH;

    public Transform player;
    public Teleport teleportationSystem;
    public SteamVR_LaserPointer laserPointer;

    public GameObject remote;
    public GameObject tv;

    private IEnumerator coroutine;
    private IEnumerator coroutine2;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;

        sceneH = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("House"));

        if (player == null)
            player = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log(e.target.tag);
        if (e.target.tag == "UserInterface")
        {
            Debug.Log("Button was clicked");
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        Debug.Log(e.target.tag);
        if (e.target.tag == "UserInterface")
        {
            Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Debug.Log(e.target.tag);
        if (e.target.tag == "UserInterface")
        {
            Debug.Log("Button was exited");
        }
    }

    public void FadeToBlack(float duration)
    {
        Debug.Log("Fading to black.");
        SteamVR_Fade.Start(Color.clear, 0f);

        SteamVR_Fade.Start(Color.black, duration);
    }

    public void FadeFromBlack(float duration)
    {
        Debug.Log("Fading from black.");
        SteamVR_Fade.Start(Color.black, 0f);

        SteamVR_Fade.Start(Color.clear, duration);
    }

    private IEnumerator WaitandMovePlayer(float waitTime, Vector3 pos, Quaternion rot)
    {
        yield return new WaitForSeconds(waitTime);
        player.position = pos;
        player.rotation = rot;
        FadeFromBlack(5f);
        Debug.Log("Coroutine Ended");
    }

    private IEnumerator LoadTutorial(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Loading Tutorial");
        SceneManager.UnloadSceneAsync("Kitchen");
        SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
        remote.SetActive(true);
        tv.SetActive(true);
    }

    public IEnumerator ResetTutorial(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Resetting Tutorial");
        SceneManager.UnloadSceneAsync("Tutorial");
        SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
    }

    private IEnumerator UnloadTutorial(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Unloading Tutorial");
        SceneManager.UnloadSceneAsync("Tutorial");
        SceneManager.LoadSceneAsync("Kitchen", LoadSceneMode.Additive);
        remote.SetActive(false);
        tv.SetActive(false);
    }


    public void Tutorial()
    {
        teleportationSystem.enabled = false;
        FadeToBlack(3f);
        coroutine = WaitandMovePlayer(5f, new Vector3(1.677f, 0f, -9.017f), Quaternion.Euler(0f, 31.364f, 0f));
        StartCoroutine(coroutine);
        coroutine2 = LoadTutorial(2f);
        StartCoroutine(coroutine2);
        teleportationSystem.enabled = true;
        ResetDontDestroyOnLoad.ResetDestroyOnLoad();
    }

    public void TutorialReset()
    {
        teleportationSystem.enabled = false;
        FadeToBlack(3f);
        coroutine = WaitandMovePlayer(5f, new Vector3(1.677f, 0f, -9.017f), Quaternion.Euler(0f, 31.364f, 0f));
        StartCoroutine(coroutine);
        coroutine2 = ResetTutorial(2f);
        StartCoroutine(coroutine2);
        teleportationSystem.enabled = true;
        ResetDontDestroyOnLoad.ResetDestroyOnLoad();
    }


    public void EndTutorial()
    {
        teleportationSystem.enabled = false;
        FadeToBlack(3f);
        coroutine = WaitandMovePlayer(5f, new Vector3(2.251f, 0f, -10.925f), Quaternion.Euler(0f, 134.71f, 0f));
        StartCoroutine(coroutine);
        coroutine2 = UnloadTutorial(2f);
        StartCoroutine(coroutine2);
        teleportationSystem.enabled = true;
        ResetDontDestroyOnLoad.ResetDestroyOnLoad();
    }

    public void Difficulty()
    {
        ;
    }

    public void Options()
    {
        ;
    }

    public void Reset()
    {
        Debug.Log("Resetting Scene");
        SceneManager.UnloadSceneAsync("Kitchen");
        SceneManager.LoadSceneAsync("Kitchen", LoadSceneMode.Additive);
        ResetDontDestroyOnLoad.ResetDestroyOnLoad();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}