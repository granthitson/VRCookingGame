using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private BoxCollider destroyZone;
    private List<GameObject> trashObjects;

    // Start is called before the first frame update
    void Start()
    {
        destroyZone = GetComponent<BoxCollider>();
        trashObjects = new List<GameObject>();
    }

    private void DestroyTrash()
    {
        Debug.Log("Destroying Trash");
        foreach (var t in trashObjects)
        {
            if (t.CompareTag("Food") || t.CompareTag("clone") || t.CompareTag("trash"))
                Destroy(t);
        }
        trashObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food") || other.gameObject.CompareTag("clone") || other.gameObject.CompareTag("trash"))
            trashObjects.Add(other.gameObject);

        if (trashObjects.Count > 3)
        {
            DestroyTrash();
        }
    }
}
