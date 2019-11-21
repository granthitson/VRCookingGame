using UnityEngine;
using System.Collections;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class KnifeMeshCutting : MonoBehaviour {

	public Material capMaterial;
    public SteamVR_Action_Boolean knifeCut;

    public float length;
    public float gap;

    public BoxCollider bladeCollider;

    private bool knifeToggle;

    private BoxCollider bladeTrigger;

    private bool enter;
    private bool exit;

    private void Start()
    {
        bladeTrigger = GetComponent<BoxCollider>();
    }

    void Update()
    {
        knifeToggle = knifeCut.GetState(SteamVR_Input_Sources.RightHand);
        if (knifeToggle == true)
        {
            Debug.Log(knifeToggle);
            bladeCollider.enabled = false;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                GameObject victim = hit.collider.gameObject;
                if (victim.tag == "Food")
                {
                    if (victim.GetComponent<Food>().capMaterial != null && enter == true && exit == true)
                    {
                        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, victim.GetComponent<Food>().capMaterial);

                        CreateObjectComponets(pieces);
                    }

                    if (victim.GetComponent<Food>().capMaterial == null && enter == true && exit == true)
                    {
                        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

                        CreateObjectComponets(pieces);
                    }

                    ResetTriggers();
                }
            }
        }
        else
        {
            bladeCollider.enabled = true;
        }
	}

    private void CreateObjectComponets(GameObject[] pieces)
    {
        if (!pieces[1].GetComponent<Rigidbody>())
        {
            pieces[1].tag = "Food";
            pieces[1].AddComponent<Rigidbody>();
            pieces[1].AddComponent<Interactable>();
            pieces[1].AddComponent<Food>();
            Throwable temp = pieces[1].AddComponent<Throwable>();
            temp.attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand;
            temp.attachmentFlags = Hand.AttachmentFlags.ParentToHand;
            temp.attachmentFlags = Hand.AttachmentFlags.VelocityMovement;
            pieces[1].AddComponent<VelocityEstimator>();
            MeshCollider temp1 = pieces[1].AddComponent<MeshCollider>();
            temp1.convex = true;
        }
        MeshCollider left = pieces[0].GetComponent<MeshCollider>();
        Destroy(left);
        MeshCollider temp2 = pieces[0].AddComponent<MeshCollider>();
        temp2.convex = true;
    }

    private void ResetTriggers()
    {
        enter = false;
        exit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (knifeToggle)
        {
            if (other.tag == "Food")
            {
                enter = true;
                Debug.Log("enter");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (knifeToggle)
        {
            if (other.tag == "Food")
            {
                exit = true;
                Debug.Log("exit");
            }
        }
    }

    public void ActivateSet()
    {
        knifeCut.actionSet.Activate();
    }

    public void DeactivateSet()
    {
        knifeCut.actionSet.Deactivate();
    }

    void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);
		Gizmos.DrawLine(transform.position + transform.up * gap, transform.position + transform.up * gap + transform.forward * length);
		Gizmos.DrawLine(transform.position + -transform.up * gap, transform.position + -transform.up * gap + transform.forward * length);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * gap);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * gap);

	}

}
