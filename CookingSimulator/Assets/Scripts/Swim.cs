//https://docs.unity3d.com/ScriptReference/Transform.Translate.html
//https://docs.unity3d.com/ScriptReference/Transform.Rotate.html

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swim : MonoBehaviour
{
    public Camera cameraVR;
    public GameObject leftController;
    public GameObject rightController;
    public Text text;

    private Vector3 cameraPos;
    private Vector3 cameraOrientationEuler;
    private Quaternion cameraOrientation;

    private Transform leftTransform;
    private List<Vector3> lastLeftPosition;

    private Transform rightTransform;
    private List<Vector3> lastRightPosition;

    private float speedCap = .3f;
    private Vector3 movement = Vector3.zero;
    private Vector3 headAxis;
    private Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        lastLeftPosition = new List<Vector3>();
        lastRightPosition = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        //get orientation of camera
        cameraPos = cameraVR.transform.position;
        cameraOrientationEuler = new Vector3(0, cameraVR.transform.eulerAngles.y, 0);
        cameraOrientation = Quaternion.Euler(cameraOrientationEuler);

        //get transforms of controllers
        leftTransform = leftController.transform;
        //Debug.Log("left controller" + leftTransform.position);
        rightTransform = rightController.transform;
        //Debug.Log("Right controller" + rightTransform.position);

        //add positions to list
        lastLeftPosition.Add(leftTransform.position);
        lastRightPosition.Add(rightTransform.position);

        //determine what axis is forward
        Vector3 headAxis = cameraVR.transform.forward;

        text.text = "X Axis: " + headAxis;

        if (lastLeftPosition.Count > 0)
        {
            CalculateMovementL();
            CalculateMovementR();
        }
            
    }

    private void CalculateMovementL()
    {
        //Debug.Log(leftTransform.position + " " + lastLeftPosition[lastLeftPosition.Count - 1] + " " + lastLeftPosition[0] + " differnce - " + (lastLeftPosition[lastLeftPosition.Count - 1].x - lastLeftPosition[0].x));

        int last = lastLeftPosition.Count - 1;
        float difference = Vector3.Distance(lastLeftPosition[last], lastLeftPosition[0]);
        Vector3 vDifference = Abs(lastLeftPosition[last] - lastLeftPosition[0]);
        //Vector3 newDifference = Abs(new Vector3(difference.x, 0f, difference.z));
        if (difference > .2)
        {
            //Debug.Log((lastLeftPosition[last].z - lastLeftPosition[0].z));
            if ((movement.x < speedCap) && (movement.z < speedCap))
            {
                temp = new Vector3(1, 0, 1);
                movement = cameraOrientation * temp * Time.deltaTime;
                movement += (movement * 1);
            }
            Debug.Log(movement);
            //Debug.Log("Moving" + lastLeftPosition[last] + " " + lastLeftPosition[0] + " " + movement.magnitude);
        }
        

        if (lastLeftPosition.Count > 25)
        {
            //Debug.Log("Removing last item: " + lastLeftPosition[lastLeftPosition.Count - 1]);
            lastLeftPosition.Remove(lastLeftPosition[0]);
        }

        transform.position += movement;

        movement *= .999f;

        if (lastLeftPosition.Count > 6)
            lastLeftPosition.RemoveRange(0, 5);
    }

    private void CalculateMovementR()
    {
        //Debug.Log(leftTransform.position + " " + lastLeftPosition[lastLeftPosition.Count - 1] + " " + lastLeftPosition[0] + " differnce - " + (lastLeftPosition[lastLeftPosition.Count - 1].x - lastLeftPosition[0].x));

        int last = lastRightPosition.Count - 1;
        float difference = Vector3.Distance(lastRightPosition[last], lastRightPosition[0]);
        Vector3 vDifference = Abs(lastRightPosition[last] - lastRightPosition[0]);
        //Vector3 newDifference = Abs(new Vector3(difference.x, 0f, difference.z));
        if (difference > .2)
        {
            //Debug.Log((lastLeftPosition[last].z - lastLeftPosition[0].z));
            if ((movement.x < speedCap) && (movement.z < speedCap))
            {
                temp = new Vector3(1, 0, 1);
                movement = cameraOrientation * temp * Time.deltaTime;
                movement += (movement * 1);
            }
            Debug.Log(movement);
            //Debug.Log("Moving" + lastLeftPosition[last] + " " + lastLeftPosition[0] + " " + movement.magnitude);
        }

        if (lastRightPosition.Count > 25)
        {
            //Debug.Log("Removing last item: " + lastLeftPosition[lastLeftPosition.Count - 1]);
            lastRightPosition.Remove(lastRightPosition[0]);
        }

        transform.position += movement;

        movement *= .99f;

        if (lastRightPosition.Count > 6)
            lastRightPosition.RemoveRange(0, 5);
    }

    private Vector3 Abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    private Vector3 Abs(float x, float y, float z)
    {
        return new Vector3(Mathf.Abs(x), Mathf.Abs(y), Mathf.Abs(z));
    }
}
