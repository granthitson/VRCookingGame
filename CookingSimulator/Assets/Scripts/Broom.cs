using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    private BoxCollider destroyZone;
    private Dictionary<int, int> trashObjects;

    // Start is called before the first frame update
    void Start()
    {
        destroyZone = GetComponent<BoxCollider>();
        trashObjects = new Dictionary<int, int>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "trash" || collision.collider.tag == "clone")
        {
            int id = collision.collider.GetInstanceID();
            if (trashObjects.ContainsKey(id))
            {
                int timesHit = trashObjects[id];
                if (timesHit > 5)
                {
                    Destroy(collision.collider.gameObject);
                    trashObjects.Remove(id);
                }
                else
                {
                    trashObjects[id] = timesHit + 1;
                }
            }
            else
            {
                int timesHit = 0;
                trashObjects.Add(id, timesHit);
            }
        }
    }
}
