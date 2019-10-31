using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class LaserInput : MonoBehaviour
{
    public SteamVR_Action_Boolean click;
    public static GameObject currentObject;
    int currentID;

    private bool onClick;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        onClick = click.GetState(SteamVR_Input_Sources.RightHand);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            int id = hit.collider.gameObject.GetInstanceID();

            if (currentID != id)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;

                if (onClick == true)
                {
                    if (currentObject.GetComponent<Button>() != null)
                    {
                        Button button = currentObject.GetComponent<Button>();
                        button.onClick.Invoke();
                    }
                }
            }

        }
    }
}
