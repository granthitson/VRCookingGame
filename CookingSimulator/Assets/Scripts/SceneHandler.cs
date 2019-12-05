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
    public static GameObject playerObject;
    public static Hand rightHand;
    public static Hand leftHand;
    public bool playedTutorial = false;

    public Transform player;
    public Teleport teleportationSystem;

    public GameObject remoteLiving;

    private IEnumerator coroutine;
    private IEnumerator coroutine2;
    private IEnumerator coroutine3;

    private Vector3 remoteLivingOriginalPosition;
    private Vector3 remoteLivingOriginalRotation;

    public GameObject tvMenu;

    private Player playerScript;
    private Vector3 playerToPlayerHeadOffset;
    private float playerToPlayerHeadRotationOffset;

    public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Action_Boolean clickAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Click");
    public SteamVR_Action_Boolean cutAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Cut");
    public SteamVR_Action_Boolean interactUIAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

    void Awake()
    {
        sceneH = this;
        playerObject = GameObject.Find("Player").GetComponent<Transform>().gameObject;
        rightHand = playerObject.transform.Find("SteamVRObjects").Find("RightHand").GetComponent<Hand>();
        leftHand = playerObject.transform.Find("SteamVRObjects").Find("LeftHand").GetComponent<Hand>();
    }

    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("House"));

        if (player == null)
            player = GameObject.Find("Player").GetComponent<Transform>();

        if (teleportationSystem == null)
            teleportationSystem = GameObject.Find("Teleporting").GetComponent<Teleport>();

        if (remoteLiving == null)
            remoteLiving = GameObject.Find("RemoteLiving").GetComponent<GameObject>();

        playerScript = player.GetComponent<Player>();

        remoteLivingOriginalPosition = remoteLiving.transform.position;
        remoteLivingOriginalRotation = remoteLiving.transform.eulerAngles;
    }

    private void Update()
    {
        CalculatePlayerToPlayerHeadOffset();
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

    private void CalculatePlayerToPlayerHeadOffset()
    {
        Vector3 playPos = player.position;
        Vector3 headPos = playerScript.headCollider.GetComponent<Transform>().position;
        Vector3 headRot = playerScript.headCollider.GetComponent<Transform>().eulerAngles;

        playerToPlayerHeadOffset = new Vector3(headPos.x, 0f, headPos.z) - playPos;
        playerToPlayerHeadRotationOffset = headRot.y - player.eulerAngles.y;
        Debug.DrawLine(new Vector3(headPos.x, 0f, headPos.z), playPos);
    }

    private IEnumerator WaitandMovePlayer(float waitTime, Vector3 pos, Vector3 rot)
    {
        //change move from the actual player gameobject to steamvrobject
        yield return new WaitForSeconds(waitTime);
        player.eulerAngles = rot;
        CalculatePlayerToPlayerHeadOffset();
        Vector3 temp = pos - playerToPlayerHeadOffset;
        player.position = temp;
        FadeFromBlack(5f);
        Debug.Log("Coroutine Ended");
    }

    private IEnumerator LoadTutorial(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Loading Tutorial");
        Scene s = SceneManager.GetSceneByName("Kitchen");
        if (s != null)
            SceneManager.UnloadSceneAsync(s);
        SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
        teleportationSystem.enabled = true;
        tvMenu.SetActive(false);

    }

    public IEnumerator ResetTutorial(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Resetting Tutorial");
        SceneManager.UnloadSceneAsync("Tutorial");
        SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
        teleportationSystem.enabled = true;
    }

    private IEnumerator UnloadTutorial(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Unloading Tutorial");
        SceneManager.UnloadSceneAsync("Tutorial");
        SceneManager.LoadSceneAsync("Kitchen", LoadSceneMode.Additive);
        teleportationSystem.enabled = true;
        tvMenu.SetActive(true);
    }

    public void ResetRemotePosition()
    {
        remoteLiving.transform.position = remoteLivingOriginalPosition;
        remoteLiving.transform.eulerAngles = remoteLivingOriginalRotation;
    }

    public void ResetPlates()
    {
        foreach (var plate in gameObject.scene.GetRootGameObjects())
        {
            if (plate.name == "plateBreak(Clone)" || plate.name == "mugBreak(Clone)" || plate.name == "bowlBreak(Clone)")
            {
                Debug.Log("Destroying" + plate.name);
                Destroy(plate);
            }
        }
    }

    public void DestroyTrash()
    {
        ResetDontDestroyOnLoad.instance.ResetDestroyOnLoad();

        var clones = GameObject.FindGameObjectsWithTag("clone");
        foreach (var clone in clones)
            Destroy(clone);

        var trash = GameObject.FindGameObjectsWithTag("trash");
        foreach (var t in trash)
            Destroy(t);
    }


    public void Tutorial()
    {
        Debug.Log("Loading Tutorial");
        teleportationSystem.enabled = false;
        FadeToBlack(3f);
        coroutine = WaitandMovePlayer(5f, new Vector3(1.677f, 0f, -9.017f), new Vector3(0f, 31.364f, 0f));
        StartCoroutine(coroutine);
        coroutine2 = LoadTutorial(2f);
        StartCoroutine(coroutine2);
    }

    public void TutorialReset()
    {
        teleportationSystem.enabled = false;
        FadeToBlack(3f);
        coroutine = WaitandMovePlayer(5f, new Vector3(1.677f, 0f, -9.017f), new Vector3(0f, 31.364f, 0f));
        StartCoroutine(coroutine);
        coroutine2 = ResetTutorial(2f);
        StartCoroutine(coroutine2);
        DestroyTrash();
    }


    public void EndTutorial()
    {
        teleportationSystem.enabled = false;
        FadeToBlack(3f);
        coroutine = WaitandMovePlayer(5f, new Vector3(2.251f, 0f, -10.925f), new Vector3(0f, 125.284f, 0f));
        StartCoroutine(coroutine);
        coroutine2 = UnloadTutorial(2f);
        StartCoroutine(coroutine2);
        DestroyTrash();
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
        DestroyTrash();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}