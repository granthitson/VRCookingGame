using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrder : MonoBehaviour
{
    public int limit;
    public Transform spawnArea;
    private List<Vector3> possibleSpawns;
    private Dictionary<int, int> spawnLimit;
    private AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        spawnLimit = new Dictionary<int, int>();
        possibleSpawns = new List<Vector3>();
        for (int i = 0; i < 3; i++)
        {
            float initialX = -.17f;
            float initialZ = -.17f;
            for (int j = 0; j < 3; j++)
            {
                if (i == 1)
                    possibleSpawns.Add(spawnArea.position + new Vector3(initialX, 0, initialZ));
                else if (i == 2)
                    possibleSpawns.Add(spawnArea.position + new Vector3(initialX, 0, 0));
                else
                    possibleSpawns.Add(spawnArea.position + new Vector3(initialX, 0, -initialZ));

                initialX += .17f;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < possibleSpawns.Count; i++)
        {
            Debug.DrawLine(spawnArea.position, possibleSpawns[i]);
        }
            
    }

    public void Order(GameObject prefab)
    {
        int id = prefab.GetInstanceID();
        if (spawnLimit.ContainsKey(id))
        {
            int spawnsLeft = spawnLimit[id];
            if (spawnsLeft > 1)
            {
                for (int i = 0; i < possibleSpawns.Count; i++)
                {
                    bool valid = true;

                    Collider[] colliders = Physics.OverlapBox(possibleSpawns[i], new Vector3(.1f,.1f,.1f));
                    foreach (Collider col in colliders)
                    {
                        if (col.tag == "Food")
                        {
                            valid = false;
                        }
                    }

                    if (valid)
                    {
                        Debug.Log("spawning");
                        Instantiate(prefab, possibleSpawns[i], spawnArea.rotation, spawnArea);
                        spawnLimit[id] = limit - 1;
                        aSource.PlayOneShot(aSource.clip);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < possibleSpawns.Count; i++)
            {
                bool valid = true;
                
                Collider[] colliders = Physics.OverlapBox(possibleSpawns[i], new Vector3(.15f, .15f, .15f));
                foreach (Collider col in colliders)
                {
                    if (col.tag == "Food")
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    Debug.Log("spawning");
                    Instantiate(prefab, possibleSpawns[i], spawnArea.rotation, spawnArea);
                    spawnLimit[id] = limit - 1;
                    aSource.PlayOneShot(aSource.clip);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
