using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butter : Food
{
    // Start is called before the first frame update
    void Start()
    {
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        zScale = transform.localScale.z;

        butterValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (onPan && temperatureValue > 0)
        {
            float randLimit = .6f * temperatureValue;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-randLimit, randLimit), Random.Range(0, randLimit), Random.Range(-randLimit, randLimit)));
            Cookware pan = oth.GetComponentInParent<Cookware>();
            if (pan.GetHeatValue() > 0)
            {
                xScale -= pan.GetHeatValue() * .0001f;
                yScale -= pan.GetHeatValue() * .0003f;
                zScale += pan.GetHeatValue() * .0002f;

                butterValue = yScale;

                if (transform.localScale.y <= 0)
                {
                    Destroy(gameObject);
                    Debug.Log("Destryed");
                }
                else
                    transform.localScale = new Vector3(xScale, yScale, zScale);
            }
        }
    }
}
