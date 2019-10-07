using UnityEngine;
using System.Collections;
using Valve.VR;

public class ExampleUseof_MeshCut : MonoBehaviour {

	public Material capMaterial;

    public SteamVR_Action_Boolean knifeCut;

    private bool knifeToggle;
	
	void Update()
    {
        knifeToggle = knifeCut.GetState(SteamVR_Input_Sources.RightHand);
        if (knifeToggle == true){
			RaycastHit hit;

			if(Physics.Raycast(transform.position, transform.forward, out hit)){

				GameObject victim = hit.collider.gameObject;
                if (victim.tag == "Food")
                {
                    GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

                    if (!pieces[1].GetComponent<Rigidbody>())
                        pieces[1].AddComponent<Rigidbody>();

                    Destroy(pieces[1], 1);
                }
			}

		}
	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * .15f);
		Gizmos.DrawLine(transform.position + transform.up * 0.02f, transform.position + transform.up * 0.02f + transform.forward * .15f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.02f, transform.position + -transform.up * 0.02f + transform.forward * .15f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.02f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.02f);

	}

}
