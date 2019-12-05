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

    private Vector3 cutPosition;

    private void Start()
    {
        bladeTrigger = GetComponent<BoxCollider>();
    }

    void Update()
    {
        knifeToggle = knifeCut.GetState(SteamVR_Input_Sources.RightHand);
        if (knifeToggle == true)
        {
            bladeCollider.enabled = false;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                GameObject victim = hit.collider.gameObject;
                Food food = victim.GetComponent<Food>();

                if (food != null)
                {
                    if (food.capMaterial != null && enter == true && exit == true)
                    {
                        cutPosition = hit.point;

                        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, food.capMaterial);

                        CreateObjectComponets(pieces);
                    }

                    if (victim.GetComponent<Food>().capMaterial == null && enter == true && exit == true)
                    {
                        cutPosition = hit.point;

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
        Food initial = pieces[0].GetComponent<Food>();

        if (!pieces[1].GetComponent<Rigidbody>())
        {
            pieces[1].tag = "Food";
            pieces[1].transform.parent = null;
            pieces[1].AddComponent<Rigidbody>();
            pieces[1].AddComponent<Interactable>();
            pieces[1].AddComponent<Food>();
            Throwable temp = pieces[1].AddComponent<Throwable>();
            temp.attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand;
            temp.attachmentFlags = Hand.AttachmentFlags.VelocityMovement;
            pieces[1].AddComponent<VelocityEstimator>();
            MeshCollider temp1 = pieces[1].AddComponent<MeshCollider>();
            temp1.convex = true;
        }

        if (initial is Food)
        {
            Food fTemp = pieces[1].AddComponent<Food>();
            fTemp.SetCookableState(initial.GetCookableState());
            fTemp.SetFreezableState(initial.GetFreezableState());
            fTemp.SetCuttableState(initial.GetCuttableState());

            fTemp.SetisCookingState(initial.GetisCookingState());
            fTemp.SetonPanState(initial.GetOnPanState());
            fTemp.SetButterValue(initial.GetButterValue());
            fTemp.SetTemperatureValue(initial.GetTemperatureValue());
            fTemp.SetCookingValue(initial.GetCookingValue());

            fTemp.SetUsesButterState(initial.GetUsesButterState());
            fTemp.SetButterValue(initial.GetButterValue());

            fTemp.SetHasTouchedFloor(initial.GetHasTouchedFloor());
            fTemp.SetCleanlinessValue(initial.GetCleanlinessValue());

            fTemp.SetOnAssembler(initial.GetOnAssembler());
            fTemp.SetCanCombine(initial.GetCanCombine());
            fTemp.SetIsCombined(initial.GetIsCombined());
            fTemp.SetIsParent(initial.GetIsParent());
            fTemp.SetCanBeParent(initial.GetCanBeParent());
            fTemp.SetHasParent(initial.GetHasParent());

            fTemp.capMaterial = initial.capMaterial;
        }

        MeshCollider left = pieces[0].GetComponent<MeshCollider>();
        Destroy(left);
        MeshCollider temp2 = pieces[0].AddComponent<MeshCollider>();
        temp2.convex = true;

        pieces[1].GetComponent<Rigidbody>().AddForceAtPosition(cutPosition, Vector3.up + Vector3.down);
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
