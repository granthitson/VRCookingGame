using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternate : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;

    private IEnumerator coroutine;
    private IEnumerator coroutine1;

    private bool i1 = true;
    private bool i2 = false;

    private IEnumerator WaitAndActivate(float waitTime, GameObject im, GameObject im2)
    {
        //change move from the actual player gameobject to steamvrobject
        yield return new WaitForSeconds(waitTime);
        im.SetActive(false);
        im2.SetActive(true);
        i2 = !i2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (i1 && !i2)
        {
            coroutine = WaitAndActivate(1, image1, image2);
            StartCoroutine(coroutine);
            i1 = false;
        }

        if (i2 && !i1)
        {
            coroutine1 = WaitAndActivate(1, image2, image1);
            StartCoroutine(coroutine1);
            i1 = true;
        }
    }
}
