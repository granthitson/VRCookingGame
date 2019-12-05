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

    private IEnumerator coroutine;
    private bool cancel = false;
    private bool foodOnPlate = false;
    private bool attached = false;
    private bool begun = false;
    private bool centered = false;

    private List<int> connectedBodies;

    private void Start()
    {
        addedFood = new List<GameObject>();
        connectedBodies = new List<int>();
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

        
        if (foodOnPlate && !attached)
        {
            Attach();
        }
            
        gameObject.GetComponent<Rigidbody>().WakeUp();
    }

    private void Attach()
    {
        foreach (GameObject f in addedFood)
        {
            if (f.transform.parent == null && f.GetComponent<Rigidbody>().velocity.magnitude < .01f && f.GetComponent<Food>().GetIsParent())
            {
                Debug.Log(cancel + " " + begun);
                coroutine = AttachFood(1f, f);
                if (!cancel && !begun)
                {
                    begun = true;
                    StartCoroutine(coroutine);
                    //Debug.Log("Coroutine Started");
                }
                else if (begun && cancel)
                {
                    attached = false;
                    begun = false;
                    StopAllCoroutines();
                    //Debug.Log("Coroutines Stopped");
                }
                else
                {
                    continue;
                }
            }
        }
    }

    private void Detach(GameObject f)
    {
        //f.transform.parent = null;
        f.GetComponent<Rigidbody>().isKinematic = false;
        f.GetComponent<Rigidbody>().useGravity = true;
        FixedJoint[] joints = gameObject.GetComponents<FixedJoint>();
        //foreach (FixedJoint fj in joints)
        //{
        //    Destroy(fj);
        //}
        connectedBodies.Clear();
        //Debug.Log("detached");
    }

    private IEnumerator AttachFood(float waitTime, GameObject f)
    {
        //change move from the actual player gameobject to steamvrobject
        yield return new WaitForSeconds(waitTime);
        //f.transform.parent = parentPoint;
        Rigidbody rb = f.GetComponent<Rigidbody>();

        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
        fj.connectedBody = rb;
        fj.enableCollision = false;
        fj.breakForce = 300f;

        rb.isKinematic = true;
        rb.useGravity = false;

        Rigidbody connected = f.GetComponent<FixedJoint>().connectedBody;
        connectedBodies.Add(connected.GetInstanceID());

        //issue is that connecting bodies can have more than one fixed joint, which results in a cyclic loop
        while (connected != null)
        {
            Rigidbody tempConnected = connected.GetComponent<FixedJoint>().connectedBody;
            if (!connectedBodies.Contains(tempConnected.GetInstanceID()))
            {
                //Debug.Log("making " + connected.name + " kinematic");
                connected.isKinematic = true;
                connected.useGravity = false;
                //connected.transform.parent = rb.gameObject.transform;
            }
            
        }

        attached = true;

        //Debug.Log("Coroutine Ended");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Food f = collision.collider.GetComponent<Food>();

        if (f != null)
        {
            if (selectedRecipe != null && f != null && !addedFood.Contains(f.gameObject))
            {
                addedFood.Add(f.gameObject);
                //Debug.Log("COllider entering");
                foodOnPlate = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Food f = collision.collider.GetComponent<Food>();

        if (selectedRecipe != null)
        {
            if (f != null)
            {
                cancel = false;
                foodOnPlate = false;
                begun = false;
                attached = false;
                Detach(f.gameObject);
                //Debug.Log("COllider exiting");
            }
        }
    }

}
