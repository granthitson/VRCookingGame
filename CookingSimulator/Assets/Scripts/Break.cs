using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public int minForce;//The Minimum Force Required To Break The Object
    public GameObject broken;//The Broken Object
    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)//When Object Collides
    {
        if (col.relativeVelocity.magnitude >= minForce)//If The magnitude Of The Collision Is Equal or Greater Than The Minimum Force Required TO Break It
        {
            Shatter();
        }
    }
    void Shatter()
    {
        Instantiate(broken, transform.position, transform.rotation);//Instantiate The Broken Version Of The Object At The Current Position 
        rigid.AddExplosionForce(1f, transform.position, 1f);
        Destroy(this.gameObject);//And Delete the Current GameObject
    }
}