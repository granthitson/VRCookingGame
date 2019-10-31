using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    BoxCollider contents;

    // Start is called before the first frame update
    void Start()
    {
        contents = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = gameObject.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
