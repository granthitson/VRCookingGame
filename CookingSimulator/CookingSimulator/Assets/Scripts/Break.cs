using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public float minForce;
    public GameObject broken;
    private AudioSource aSource;
    private Rigidbody rigid;

    private bool stop;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        broken.tag = "trash";
        aSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)//When Object Collides
    {
        if (stop == false && col.relativeVelocity.magnitude >= minForce)//If The magnitude Of The Collision Is Equal or Greater Than The Minimum Force Required TO Break It
        {
            Shatter();
            stop = true;
        }
    }

    void Shatter()
    {
        AudioSource.PlayClipAtPoint(aSource.clip, transform.transform.position, .3f);
        GameObject obj = Instantiate(broken, transform.position, transform.rotation);//Instantiate The Broken Version Of The Object At The Current Position 
        Rigidbody[] rB = obj.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody piece in rB)
        {
            piece.velocity = rigid.velocity;
        }
        transform.parent = null;
        Destroy(this.gameObject);//And Delete the Current GameObject
    }
}