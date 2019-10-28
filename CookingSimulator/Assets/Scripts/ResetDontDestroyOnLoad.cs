using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetDontDestroyOnLoad : MonoBehaviour
{
    protected Scene defaultScene;

    public static void ResetDestroyOnLoad()
    {
        var clones = GameObject.FindGameObjectsWithTag("clone");
        foreach (var clone in clones)
            Destroy(clone);

        var go = new GameObject("Sacrifice");
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
        {
            if (root.name == "Player" || root.name == "[ChaperoneInfo]" || root.name == "EventSystem")
            {
                ;
            }
            else
            {
                Debug.Log("Destroying" + root.name);
                Destroy(root);
            }
        }

    }
}
