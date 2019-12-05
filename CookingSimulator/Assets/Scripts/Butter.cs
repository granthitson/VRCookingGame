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
    protected override void CookingUpdate()
    {
        if (onPan && temperatureValue > 0)
        {
            float randLimit = .6f * temperatureValue;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-randLimit, randLimit), Random.Range(0, randLimit), Random.Range(-randLimit, randLimit)));
            Cookware pan = oth.GetComponentInParent<Cookware>();
            if (pan.GetHeatValue() > 0)
            {
                Vector3 down = -transform.up;

                float hv = pan.GetHeatValue();

                if (V3Equal(down, Vector3.up) || V3Equal(down, Vector3.down))
                {
                    xScale -= hv * .0001f;
                    yScale -= hv * .0004f;
                    zScale += hv * .0003f;
                }
                else if (V3Equal(down, Vector3.left) || V3Equal(down, Vector3.right))
                {
                    xScale -= hv * .0004f;
                    yScale += hv * .0001f;
                    zScale -= hv * .0002f;
                }
                else
                {
                    xScale -= hv * .0002f;
                    yScale += hv * .0001f;
                    zScale -= hv * .0005f;
                }
                
                butterValue = hv * ((xScale + yScale + zScale) / 3);

                if (transform.localScale.x <= .01 || transform.localScale.y <= .01 || transform.localScale.z <= .01)
                {
                    pan.RemoveFromListOfAllFoods(gameObject.GetInstanceID());
                    Destroy(gameObject);
                }
                else
                    transform.localScale = new Vector3(xScale, yScale, zScale);
            }
        }
    }

    private bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }
}
