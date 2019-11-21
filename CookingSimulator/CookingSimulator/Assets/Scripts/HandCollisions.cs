using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("lefthand trigger enter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("lefthand trigger exit");
    }
}
