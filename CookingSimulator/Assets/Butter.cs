using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butter : Food
{

    private float xScale;
    private float yScale;
    private float zScale;
    
    // Start is called before the first frame update
    void Start()
    {
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        zScale = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pan")
        {
            Debug.Log("Butter on pan");
            Cookware pan = other.GetComponent<Cookware>();
            xScale += pan.GetHeatValue() * .1f;
            yScale -= pan.GetHeatValue() * .1f;
            zScale += pan.GetHeatValue() * .1f;
            if (transform.localScale.y <= 0)
                Destroy(gameObject);
            else
                transform.localScale = new Vector3(xScale, yScale, zScale);
        }
        else {
            Debug.Log(other.tag);
        }
    }
}
