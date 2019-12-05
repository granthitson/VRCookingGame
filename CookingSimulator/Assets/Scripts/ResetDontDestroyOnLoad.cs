using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetDontDestroyOnLoad : MonoBehaviour
{
    public static ResetDontDestroyOnLoad instance = null;
    protected Scene defaultScene;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void ResetDestroyOnLoad()
    {
        var clones = GameObject.FindGameObjectsWithTag("clone");
        foreach (var clone in clones)
            Destroy(clone);

        var go = new GameObject("Sacrifice");
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
        {
            if (root.tag == "trash" || root.tag == "clone")
            {
                Debug.Log("Destroying" + root.name);
                Destroy(root);
            }
        }
    }
}
