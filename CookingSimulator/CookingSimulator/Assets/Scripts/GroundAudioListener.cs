using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAudioListener : MonoBehaviour
{
    public AudioClip potDrop;
    public AudioClip utensilDrop;
    public AudioClip remoteDrop;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered the ground trigger.");
        if (other.tag == "Pot" || other.tag == "Pan")
        {
            AudioSource.PlayClipAtPoint(potDrop, other.transform.position, .2f);
        }
        else if (other.tag == "Fork" || other.tag == "Spoon")
        {
            AudioSource.PlayClipAtPoint(utensilDrop, other.transform.position, .2f);
        }
        else if (other.tag == "Remote")
        {
            AudioSource.PlayClipAtPoint(remoteDrop, other.transform.position, .2f);
        }
    }
}
